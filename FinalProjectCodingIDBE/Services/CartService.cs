using FinalProjectCodingIDBE.DTOs.CartDTO;
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
        public List<CartResponseDTO> GetCartAll(int userId)
        {
            return _cartRepository.GetAllCart(userId);
        }

        public string CartCreate(AddCartDTO cartData)
        {
            return _cartRepository.CreateCart(cartData);
        }
        public string CartDelete(int idUser, int idCart)
        {
            return _cartRepository.DeleteCart(idUser, idCart);
        }
    }
}
