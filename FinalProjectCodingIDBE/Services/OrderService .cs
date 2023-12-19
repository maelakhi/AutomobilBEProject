using FinalProjectCodingIDBE.DTOs.CartDTO;
using FinalProjectCodingIDBE.DTOs.OrderDTO;
using FinalProjectCodingIDBE.Repositories;

namespace FinalProjectCodingIDBE.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public List<OrderResponseDTO> GetOrderAll(int userId)
        {
            return _orderRepository.GetAllOrders(userId);
        }

       /* public string CartCreate(AddCartDTO cartData)
        {
            return _cartRepository.CreateCart(cartData);
        }
        public string CartDelete(int idUser, int idCart)
        {
            return _cartRepository.DeleteCart(idUser, idCart);
        }*/
    }
}
