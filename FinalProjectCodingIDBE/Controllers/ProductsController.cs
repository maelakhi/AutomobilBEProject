using FinalProjectCodingIDBE.DTOs.ProductDTO;
using FinalProjectCodingIDBE.Models;
using FinalProjectCodingIDBE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        [HttpGet("/productsActived")]
        public ActionResult GetAllActived()
        {
            return Ok(_productService.GetAllProductsActived());
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

        [Authorize(Roles = "admin")]
        [HttpPost("/products")]
        public async Task<ActionResult> CreateProduct([FromForm] AddProductsDTO addProductsDTO)
        {
            IFormFile image = addProductsDTO.Image!;
            Console.WriteLine(image);


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
                      message = "Success Add Product"
                  }
              );
        }


        [Authorize(Roles = "admin")]
        [HttpPut("/products")]
        public async Task<ActionResult> UpdatedProduct([FromForm] EditProductsDTO editProductsDTO)
        {
            ProductsResponseDTO productExist = _productService.GetByIdProducts(editProductsDTO.ProductID);
            string fileUrlPath = productExist.ImagePath;
            IFormFile image = editProductsDTO.Image!;

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

                string fileExistName = productExist.ImagePath.Replace("https://localhost:7052/uploads/", "");
                string imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, physicalPath, fileExistName);
                
                // remove image from server
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

            }


            string res = _productService.ProductUpdate(editProductsDTO.ProductID, editProductsDTO, fileUrlPath);
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
                    message = "Success Update Product"
                }
            );
        }

        [Authorize(Roles = "admin")]
        [HttpPut("/products/Deactived")]
        public ActionResult DeactivedProduct([FromBody] int Id)
        {
            string res = _productService.ProductUpdateStatus(Id, false);
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
                    message = "Success Deactiated Course"
                }
            );
        }

        [Authorize(Roles = "admin")]
        [HttpPut("/products/Actived")]
        public ActionResult ActivedProduct([FromBody] int Id)
        {
            string res = _productService.ProductUpdateStatus(Id, true);
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
                    message = "Success Actiated Course"
                }
            );
        }


        [Authorize]
        //[Authorize(Roles = "admin")]
        [HttpDelete("/products")]
        public ActionResult DeleteProduct([FromBody] int Id)
        {
            string res = _productService.ProductDelete(Id);
            if(string.IsNullOrEmpty(res) == false)
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
