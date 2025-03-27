using Labb2Webb.Shared.DTOs;

namespace BlazorFrontend.Services
{
    public class CartService
    {
        private readonly List<CartItem> _cartItems = new();
        public IReadOnlyList<CartItem> CartItems => _cartItems;

        public event Action OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        public void AddToCart(ProductDto product, int quantity = 1)
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
            NotifyStateChanged();
        }

        public void RemoveFromCart(ProductDto product)
        {
            var item = _cartItems.FirstOrDefault(x => x.Product.Id == product.Id);
            if (item != null)
            {
                _cartItems.Remove(item);
                NotifyStateChanged();
            }
        }

        public void ClearCart()
        {
            _cartItems.Clear();
            NotifyStateChanged();
        }
    }

    public class CartItem
    {
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
