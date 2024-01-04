using FinalProjectCodingIDBE.DTOs.CartDTO;
using FinalProjectCodingIDBE.DTOs.OrderDTO;
using FinalProjectCodingIDBE.DTOs.PaymentDTO;
using FinalProjectCodingIDBE.DTOs.ProductDTO;
using FinalProjectCodingIDBE.Models;
using FinalProjectCodingIDBE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        [Authorize(Roles = "admin")]
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
                      message = "Success Add Payment Method"
                  }
              );
        }

        [Authorize]
        [Authorize(Roles = "admin")]
        [HttpPut("/paymentMethod")]
        public async Task<ActionResult> UpdatedPayment([FromForm] EditPaymentDTO paymentDTO)
        {
            PaymentMethod paymentExist = _paymentService.GetPaymentMethodById(paymentDTO.paymentID);
            string fileUrlPath = paymentExist.ImagePath;
            IFormFile image = paymentDTO.Image!;

            if (string.IsNullOrEmpty(image?.FileName) == false)
            {
                var extName = Path.GetExtension(image.FileName).ToLowerInvariant(); //.jpg

                string fileName = Guid.NewGuid().ToString() + extName;
                string uploadDir = "uploads";
                string physicalPath = $"wwwroot/{uploadDir}";

                var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, physicalPath, fileName);

                using var stream = System.IO.File.OpenWrite(filePath);
                await image.CopyToAsync(stream);

                fileUrlPath = $"https://localhost:7052/{uploadDir}/{fileName}";

                string fileExistName = paymentExist.ImagePath.Replace("https://localhost:7052/uploads/", "");
                string imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, physicalPath, fileExistName);

                // remove image from server
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

            }


            string res = _paymentService.UpdatePayment(paymentDTO.paymentID, paymentDTO, fileUrlPath);
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
                    message = "Success Update Payment Method"
                }
            );
        }

        [Authorize]
        /*[Authorize(Roles = "admin")]*/
        [HttpDelete("/paymentMethod")]
        public ActionResult DeletePayment([FromBody] int Id)
        {
            string res = _paymentService.DeletePayment(Id);
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
                   message = "SuccessFull Delete"
               }
           );
        }

        [Authorize(Roles = "admin")]
        [HttpPut("/paymentMethod/Deactived")]
        public ActionResult DeactivatePayment([FromBody] int Id)
        {
            string res = _paymentService.PaymentStatus(Id, false);
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
                    message = "Success Deactivate Payment Method"
                }
            );
        }

        [Authorize(Roles = "admin")]
        [HttpPut("/paymentMethod/Actived")]
        public ActionResult ActivatePayment([FromBody] int Id)
        {
            string res = _paymentService.PaymentStatus(Id, true);
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
                    message = "Success Activate a Payment Method"
                }
            );
        }

    }
}
