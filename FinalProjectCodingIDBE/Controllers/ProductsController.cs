using FinalProjectCodingIDBE.DTOs.ProductDTO;
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
        public ProductsController(ProductService serviceProducts)
        {
            _productService = serviceProducts;
        }

        [HttpGet("/products")]
        public ActionResult GetAll()
        {
            return Ok(_productService.GetAllProducts());
        }

        [HttpGet("/productsLimit")]
        public ActionResult GetLimit()
        {
            return Ok(_productService.GetLimitProducts());
        }

        [HttpGet("/products/{Id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(_productService.GetByIdProducts(Id));
        }

        [HttpPost("/products")]
        public ActionResult CreateProduct([FromBody] AddProductsDTO addProductsDTO)
        {
            return Ok(_productService.ProductCreate(addProductsDTO));
        }
        [HttpPut("/products")]
        public ActionResult UpdatedProduct(int Id, [FromBody] AddProductsDTO addProductsDTO)
        {
            return Ok(_productService.ProductUpdate(Id,addProductsDTO));
        }
        [HttpDelete("/products/{Id}")]
        public ActionResult DeleteProduct(int Id)
        {
            return Ok(_productService.ProductDelete(Id));
        }

        [HttpGet("/productsByCategory/{categoryName}")]
        public ActionResult GetByCategory(String categoryName)
        {
            return Ok(_productService.GetByCategoryProducts(categoryName));
        }
    }
}
