using FinalProjectCodingIDBE.DTOs.CartDTO;
using FinalProjectCodingIDBE.Models;
using FinalProjectCodingIDBE.Repositories;

namespace FinalProjectCodingIDBE.Services
{
    public class CartService
    {
        private readonly CartRepository _cartRepository;
        public CartService(CartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public List<Cart> GetCartAll(int userId)
        {
            return _cartRepository.GetAllCart(userId);
        }

        public CartResponseDTO GetCartById(int Id, int userId)
        {
            return _cartRepository.GetCartById(Id, userId);
        }
        public string CartCreate(Cart cartData)
        {
            return _cartRepository.CreateCart(cartData);
        }
    }
}
