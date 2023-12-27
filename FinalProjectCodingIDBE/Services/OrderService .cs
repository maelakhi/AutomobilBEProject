using FinalProjectCodingIDBE.DTOs.CartDTO;
using FinalProjectCodingIDBE.DTOs.OrderDTO;
using FinalProjectCodingIDBE.Models;
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

        public OrderResponseDTO GetOrderById(int userId, int orderId)
        {
            return _orderRepository.GetByIdOrders(userId, orderId);
        }

        public string OrderCreate(int userId, AddOrderDTO cartData)
        {
             return _orderRepository.CreateOrder(userId,cartData);
        }

        public string CreateOrderInvoice(int userId, AddOrderDTO cartData)
        {
            return _orderRepository.CreateOrderInvoice(userId, cartData);
        }

    }
}
