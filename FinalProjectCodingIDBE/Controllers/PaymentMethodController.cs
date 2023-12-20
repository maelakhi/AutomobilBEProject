using FinalProjectCodingIDBE.Models;
using FinalProjectCodingIDBE.Services;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("/paymentMethod/{id}")]
        public IActionResult GetPaymnetById(int Id)
        {
            PaymentMethod payment = _paymentService.GetPaymentMethodById(Id);
            if (payment.Id == 0 )
            {
                return NotFound("Data Tidak di temukan");
            }
            return Ok(payment);
        }
    }
}
