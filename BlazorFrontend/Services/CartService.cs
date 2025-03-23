using Labb2Webb.Shared.DTOs;

namespace BlazorFrontend.Services
{
    public class CartService
    {
        private readonly List<ProductDto> _cartItems = new();
        public IReadOnlyList<ProductDto> CartItems => _cartItems;

        public void AddToCart(ProductDto product)
        {
            _cartItems.Add(product);
        }

        public void RemoveFromCart(ProductDto product)
        {
            _cartItems.Remove(product);
        }

        public void ClearCart()
        {
            _cartItems.Clear();
        }
    }
}
