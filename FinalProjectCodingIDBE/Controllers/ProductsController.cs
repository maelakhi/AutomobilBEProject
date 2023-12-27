using FinalProjectCodingIDBE.DTOs.ProductDTO;
using FinalProjectCodingIDBE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectCodingIDBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductsController(ProductService serviceProducts, IWebHostEnvironment webHostEnvironment)
        {
            _productService = serviceProducts;
            _webHostEnvironment = webHostEnvironment;
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

        [Authorize]
        //[Authorize(Roles = "admin")]
        [HttpPost("/products")]
        public async Task<ActionResult> CreateProduct([FromForm] AddProductsDTO addProductsDTO)
        {
            IFormFile image = addProductsDTO.Image!;

            var extName = Path.GetExtension(image.FileName).ToLowerInvariant(); //.jpg

            string fileName = Guid.NewGuid().ToString() + extName;
            string uploadDir = "uploads";
            string physicalPath = $"wwwroot/{uploadDir}";

            var filePath =Path.Combine(_webHostEnvironment.ContentRootPath, physicalPath, fileName);

            using var stream = System.IO.File.OpenWrite(filePath);
            await image.CopyToAsync(stream);

            string fileUrlPath = $"https://localhost:7052/{uploadDir}/{fileName}";


            string res = _productService.ProductCreate(addProductsDTO, fileUrlPath);
            if (string.IsNullOrEmpty(res) == false)
            {
                return BadRequest(res);
            }
            return Ok("Success Add Product");
        }

        [Authorize]
        //[Authorize(Roles = "admin")]
        [HttpPut("/products")]
        public async Task<ActionResult> UpdatedProduct(int Id, [FromForm] AddProductsDTO addProductsDTO)
        {
            IFormFile image = addProductsDTO.Image!;

            var extName = Path.GetExtension(image.FileName).ToLowerInvariant(); //.jpg

            string fileName = Guid.NewGuid().ToString() + extName;
            string uploadDir = "uploads";
            string physicalPath = $"wwwroot/{uploadDir}";

            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, physicalPath, fileName);

            using var stream = System.IO.File.OpenWrite(filePath);
            await image.CopyToAsync(stream);

            string fileUrlPath = $"https://localhost:7052/{uploadDir}/{fileName}";


            string res = _productService.ProductUpdate(Id, addProductsDTO, fileUrlPath);
            if (string.IsNullOrEmpty(res) == false)
            {
                return BadRequest(res);
            }
            return Ok("Success Update Product");
        }

        [Authorize]
        //[Authorize(Roles = "admin")]
        [HttpPut("/products/Deactived")]
        public ActionResult DeactivedProduct(int Id)
        {
            string res = _productService.ProductUpdateStatus(Id, false);
            if (string.IsNullOrEmpty(res) == false)
            {
                return BadRequest(res);
            }
            return Ok("SuccessFull Deactived");
        }

        [Authorize]
        //[Authorize(Roles = "admin")]
        [HttpPut("/products/Actived")]
        public ActionResult ActivedProduct(int Id)
        {
            string res = _productService.ProductUpdateStatus(Id, true);
            if (string.IsNullOrEmpty(res) == false)
            {
                return BadRequest(res == null) ;
            }
            return Ok("SuccessFull Deactived");
        }


        [Authorize]
        //[Authorize(Roles = "admin")]
        [HttpDelete("/products/{Id}")]
        public ActionResult DeleteProduct(int Id)
        {
            string res = _productService.ProductDelete(Id);
            if(res != null)
            {
                return BadRequest(res);
            }
            return Ok("SuccessFull Delete");
        }

        [HttpGet("/productsByCategory/{categoryName}")]
        public ActionResult GetByCategory(String categoryName)
        {
            return Ok(_productService.GetByCategoryProducts(categoryName));
        }

        [HttpGet("/productsByCategoryId/{Id}")]
         public ActionResult GetByCategoryId(int Id)
         {
            return Ok(_productService.GetByCategoryProductsId(Id));
         }
    }
}
