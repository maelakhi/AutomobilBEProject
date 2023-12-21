using FinalProjectCodingIDBE.DTOs.CartDTO;
using FinalProjectCodingIDBE.DTOs.OrderDTO;
using FinalProjectCodingIDBE.DTOs.PaymentDTO;
using FinalProjectCodingIDBE.DTOs.ProductDTO;
using FinalProjectCodingIDBE.Models;
using FinalProjectCodingIDBE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectCodingIDBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly PaymentMethodService _paymentService;

        public PaymentMethodController(PaymentMethodService paymentService)
        {
            _paymentService = paymentService;
        }

        [Authorize]
        [HttpGet("/paymentMethod")]
        public IActionResult GetAll()
        {
            List<PaymentMethod> paymentList = _paymentService.GetPaymentMethodAll();
            if (paymentList.Count < 1)
            {
                return NotFound("Data Tidak di temukan");
            }
            return Ok(paymentList);
        }

        [Authorize]
        [HttpGet("/paymentMethod/{Id}")]
        public IActionResult GetPaymnetById(int Id)
        {
            PaymentMethod payment = _paymentService.GetPaymentMethodById(Id);
            if (payment.Id == 0)
            {
                return NotFound("Data Tidak di temukan");
            }
            return Ok(payment);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("/paymentMethod")]
        public ActionResult AddPayment([FromBody] AddPaymentDTO payment)
        {
            string res = _paymentService.AddPayment(payment);
            if (string.IsNullOrEmpty(res) == false)
            {
                return BadRequest(res);
            }
            return Ok("Succesfully Add Payment Method");
        }

        [Authorize(Roles = "admin")]
        [HttpPut("/paymentMethod")]
        public ActionResult UpdatedPayment(int Id, [FromBody] AddPaymentDTO payment)
        {
            string res = _paymentService.UpdatePayment(Id, payment);
            if (!string.IsNullOrEmpty(res))
            {
                return BadRequest(res);
            }
            return Ok("Success Update Payment Method");
        }

        [HttpDelete("/paymentMethod")]
        public ActionResult DeletePayment(int Id)
        {
            string res = _paymentService.DeletePayment(Id);
            if (!string.IsNullOrEmpty(res))
            {
                return BadRequest(res);
            }
            return Ok("SuccessFull Delete");
        }

    }
}
