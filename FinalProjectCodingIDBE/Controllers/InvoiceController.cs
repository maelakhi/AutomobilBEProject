using FinalProjectCodingIDBE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalProjectCodingIDBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceService _invoiceService;

        public InvoiceController(InvoiceService serviceInvoice)
        {
            _invoiceService = serviceInvoice;
        }

        [Authorize]
        [HttpGet("/invoice")]
        public ActionResult GetAll()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid));
            return Ok(_invoiceService.GetInvoiceAll(userId));
        }

        [Authorize]
        [HttpGet("/invoice/{idInvoice}")]
        public ActionResult GetById(int idInvoice)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid));
            return Ok(_invoiceService.GetInvoiceById(userId, idInvoice));
        }

        [Authorize]
        [HttpPost("/invoice")]
        public ActionResult CreateInvoice([FromBody] int idOrder)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid));
            string res = _invoiceService.CreateInvoice(userId, idOrder);
            if (string.IsNullOrEmpty(res) == false)
            {
                return BadRequest(res);
            }
            return Ok("Success Create Invoice");
        }

    }
}
