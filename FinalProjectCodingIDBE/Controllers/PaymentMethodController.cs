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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PaymentMethodController(PaymentMethodService paymentService, IWebHostEnvironment webHostEnvironment)
        {
            _paymentService = paymentService;
            _webHostEnvironment = webHostEnvironment;
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

        [Authorize]
        /*[Authorize(Roles = "admin")]*/
        [HttpPost("/paymentMethod")]
        public async Task <ActionResult> AddPayment([FromForm] AddPaymentDTO paymentDTO)
        {
            IFormFile? image = paymentDTO.Image!;

            var extName = Path.GetExtension(image.FileName).ToLowerInvariant(); //.jpg

            string fileName = Guid.NewGuid().ToString() + extName;
            string uploadDir = "uploadsPaymentIcon";
            string physicalPath = $"wwwroot/{uploadDir}";

            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, physicalPath, fileName);

            using var stream = System.IO.File.OpenWrite(filePath);
            await image.CopyToAsync(stream);

            string fileUrlPath = $"https://localhost:7052/{uploadDir}/{fileName}";

            string res = _paymentService.AddPayment(paymentDTO, fileUrlPath);
            if (string.IsNullOrEmpty(res) == false)
            {
                return BadRequest(res);
            }
            return Ok("Succesfully Add Payment Method");
        }

        [Authorize]
        /*[Authorize(Roles = "admin")]*/
        [HttpPut("/paymentMethod")]
        public async Task<ActionResult> UpdatedPayment(int Id, [FromForm] AddPaymentDTO payment)
        {
            IFormFile image = payment.Image!;

            var extName = Path.GetExtension(image.FileName).ToLowerInvariant(); //.jpg

            string fileName = Guid.NewGuid().ToString() + extName;
            string uploadDir = "uploadsPaymentIcon";
            string physicalPath = $"wwwroot/{uploadDir}";

            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, physicalPath, fileName);

            using var stream = System.IO.File.OpenWrite(filePath);
            await image.CopyToAsync(stream);

            string fileUrlPath = $"https://localhost:7052/{uploadDir}/{fileName}";

            string res = _paymentService.UpdatePayment(Id, payment, fileUrlPath);
            if (!string.IsNullOrEmpty(res))
            {
                return BadRequest(res);
            }
            return Ok("Success Update Payment Method");
        }

        [Authorize]
        /*[Authorize(Roles = "admin")]*/
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
