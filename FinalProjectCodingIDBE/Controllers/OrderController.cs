using FinalProjectCodingIDBE.DTOs.OrderDTO;
using FinalProjectCodingIDBE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

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

        [Authorize]
        [HttpGet("/order")]
        public ActionResult GetAll()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid));
            return Ok(_serviceOrder.GetOrderAll(userId));
        }

        [Authorize]
        [HttpGet("/orderById/{orderId}")]
        public ActionResult GetByIdOrded(int orderId)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid));
            return Ok(_serviceOrder.GetOrderById(userId, orderId));
        }

        [Authorize]
        [HttpPost("/order")]
        public ActionResult CreateOrder([FromBody] AddOrderDTO addOrderDTO)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid));
            string res = _serviceOrder.OrderCreate(userId, addOrderDTO);
            if (string.IsNullOrEmpty(res) == false)
            {
                return StatusCode(
                         (int)HttpStatusCode.BadRequest,
                         new
                         {
                             status = HttpStatusCode.BadRequest,
                             message = res
                         }
                     );
            }
            return StatusCode(
                (int)HttpStatusCode.OK,
                new
                {
                    status = HttpStatusCode.OK,
                    message = "Success Get Data"
                }
            );
        }

        [Authorize]
        [HttpPost("/orderInvoice")]
        public ActionResult CreateOrderInvoice([FromBody] AddOrderDTO addOrderDTO)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid));
            string res = _serviceOrder.CreateOrderInvoice(userId, addOrderDTO);
            if (string.IsNullOrEmpty(res) == false)
            {
                return StatusCode(
                        (int)HttpStatusCode.BadRequest,
                        new
                        {
                            status = HttpStatusCode.BadRequest,
                            message = "Failed Create Invoice"
                        }
                    );
            }


            return StatusCode(
                        (int)HttpStatusCode.OK,
                        new
                        {
                            status = HttpStatusCode.OK,
                            message = "Success Create Invoice"
                        }
                    );
        }
    }
}
