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

        public CartService(ILocalStorageService localStorage, IJSRuntime jSRuntime)
        {
            _localStorage = localStorage;
            _jsRuntime = jSRuntime;
        }

        private async Task SaveCartAsync()
        {
            await _localStorage.SetItemAsync("cartItems", _cartItems);
            OnChange?.Invoke();
        }

        public async Task InitializeCartAsync()
        {
            if (_isInitialized) return;

            try
            {
                var items = await _localStorage.GetItemAsync<List<CartItem>>("cartItems");
                if (items != null)
                {
                    _cartItems.Clear();
                    _cartItems.AddRange(items);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cart deserialization failed: " + ex.Message);
                await _localStorage.RemoveItemAsync("cartItems");
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
