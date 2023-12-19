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

        [HttpGet("/order/{userId}")]
        public ActionResult GetAll(int userId)
        {
            return Ok(_serviceOrder.GetOrderAll(userId));
        }
    }
}
