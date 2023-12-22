using FinalProjectCodingIDBE.DTOs.OrderDTO;
using FinalProjectCodingIDBE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectCodingIDBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _serviceOrder;

        public OrderController(OrderService serviceOrder)
        {
            _serviceOrder = serviceOrder;
        }

        [HttpGet("/order")]
        public ActionResult GetAll(int userId)
        {
            return Ok(_serviceOrder.GetOrderAll(userId));
        }

        [HttpGet("/orderById/{orderId}")]
        public ActionResult GetByIdOrded(int userId, int orderId)
        {
            return Ok(_serviceOrder.GetOrderById(userId, orderId));
        }

        [HttpPost("/orderById")]
        public ActionResult CreateOrder(int userId, [FromBody] AddOrderDTO addOrderDTO)
        {
            string res = _serviceOrder.OrderCreate(userId, addOrderDTO);
            if (string.IsNullOrEmpty(res) == false)
            {
                return BadRequest(res);
            }
            return Ok("Success Create Invoice");
        }
    }
}
