using System.Text.Json;
using System.Text.Json.Serialization;
using Blazored.LocalStorage;
using Labb2Webb.Shared.DTOs;
using Microsoft.JSInterop;

namespace BlazorFrontend.Services
{
    public class CartService
    {
        private bool _isInitialized = false;
        private IJSRuntime _jsRuntime;
        private ILocalStorageService _localStorage;
        private readonly List<CartItem> _cartItems = new();
        public IReadOnlyList<CartItem> CartItems => _cartItems;
        public event Action OnChange;
        private string _cartKey = "cartItems";


        public CartService(ILocalStorageService localStorage, IJSRuntime jSRuntime)
        {
            _localStorage = localStorage;
            _jsRuntime = jSRuntime;
        }

        private async Task SaveCartAsync()
        {
            await _localStorage.SetItemAsync(_cartKey, _cartItems);
            OnChange?.Invoke();
        }

        public async Task InitializeCartAsync()
        {
            if (_isInitialized) return;

            string email = null;
            for (int i = 0; i < 10; i++)
            {
                email = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authEmail");
                if (!string.IsNullOrEmpty(email))
                    break;

                await Task.Delay(100);
            }

            _cartKey = !string.IsNullOrEmpty(email) ? $"cartItems_{email}" : "cartItems_guest";

            try
            {
                var storedCart = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", _cartKey);
                if (!string.IsNullOrEmpty(storedCart))
                {
                    var items = JsonSerializer.Deserialize<List<CartItem>>(storedCart, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        Converters = { new JsonStringEnumConverter() }
                    });

                    if (items != null)
                    {
                        _cartItems.Clear();
                        _cartItems.AddRange(items);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cart deserialization failed: " + ex.Message);
                await _localStorage.RemoveItemAsync(_cartKey);
            }

            _isInitialized = true;
        }

        public async Task AddToCart(ProductDto product, int quantity = 1)
        {
            var existingItem = _cartItems.FirstOrDefault(x => x.Product.Id == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                _cartItems.Add(new CartItem { Product = product, Quantity = quantity });
            }
            await SaveCartAsync();
        }

        public async Task RemoveFromCart(ProductDto product)
        {
            var item = _cartItems.FirstOrDefault(x => x.Product.Id == product.Id);
            if (item != null)
            {
                _cartItems.Remove(item);
                await SaveCartAsync();
            }
        }

        public async Task ClearCart()
        {
            _cartItems.Clear();
            await SaveCartAsync();
        }
    }

    public class CartItem
    {
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
