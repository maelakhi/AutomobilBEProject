using FinalProjectCodingIDBE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectCodingIDBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductsController(ProductService serviceProducts) { 
            _productService = serviceProducts;
        }

        [HttpGet("/products")]   
        public ActionResult GetAll()
        {
            return Ok(_productService.GetAllProducts());
        }
    }
}
