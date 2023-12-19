using FinalProjectCodingIDBE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("/invoice")]
        public ActionResult GetAll(int userId)
        {
            return Ok(_invoiceService.GetInvoiceAll(userId));
        }

        [HttpGet("/invoice/{idInvoice}")]
        public ActionResult GetById(int userId, int idInvoice)
        {
            return Ok(_invoiceService.GetInvoiceById(userId, idInvoice));
        }

        [HttpPost("/invoice")]
        public ActionResult CreateInvoice(int userId,[FromBody] int idOrder)
        {
            string res = _invoiceService.CreateInvoice(userId, idOrder);
            if (string.IsNullOrEmpty(res) == false)
            {
                return BadRequest(res);
            }
            return Ok("Success Create Invoice");
        }

    }
}
