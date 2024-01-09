using FinalProjectCodingIDBE.DTOs.CategoryDTO;
using FinalProjectCodingIDBE.DTOs.PaymentDTO;
using FinalProjectCodingIDBE.DTOs.ProductDTO;
using FinalProjectCodingIDBE.Models;
using FinalProjectCodingIDBE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace FinalProjectCodingIDBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorysController : ControllerBase
    {
        private readonly CategoryService _CategoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CategorysController(CategoryService serviceCategorys, IWebHostEnvironment webHostEnvironment)
        {
            _CategoryService = serviceCategorys;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("/Category")]
        public ActionResult GetAllUser()
        {
            return Ok(_CategoryService.GetCategoriesUser());
        }

        [HttpGet("/admin/Category")]
        public ActionResult GetAll()
        {
            return Ok(_CategoryService.GetCategories());
        }

        [HttpGet("/category/{Id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(_CategoryService.GetByIdCategory(Id));
        }

        [Authorize(Roles = "admin")]
        [HttpPost("/category")]
        public async Task<ActionResult> CreateCategory([FromForm] AddCategoryDTO addCategoryDTO)
        {
            IFormFile image = addCategoryDTO.Image!;

            var extName = Path.GetExtension(image.FileName).ToLowerInvariant(); //.jpg

            string fileName = Guid.NewGuid().ToString() + extName;
            string uploadDir = "uploads";
            string physicalPath = $"wwwroot/{uploadDir}";

            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, physicalPath, fileName);

            using var stream = System.IO.File.OpenWrite(filePath);
            await image.CopyToAsync(stream);

            string fileUrlPath = $"{uploadDir}/{fileName}";

            string res = _CategoryService.CategoryCreate(addCategoryDTO, fileUrlPath);

            if (!string.IsNullOrEmpty(res))
            {
                return BadRequest(res);
            }
            return Ok("Success Add Category");
        }

        [Authorize(Roles = "admin")]
        [HttpPut("/category")]
        public async Task<ActionResult> UpdatedCategory([FromForm] EditCategoryDTO categoryDTO)
        {
            Category categoryExist = _CategoryService.GetByIdCategory(categoryDTO.categoryID);
            string fileUrlPath = categoryExist.ImagePath;
            IFormFile image = categoryDTO.Image!;

            if (string.IsNullOrEmpty(image?.FileName) == false)
            {
                var extName = Path.GetExtension(image.FileName).ToLowerInvariant(); //.jpg

                string fileName = Guid.NewGuid().ToString() + extName;
                string uploadDir = "uploads";
                string physicalPath = $"wwwroot/{uploadDir}";

                var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, physicalPath, fileName);

                using var stream = System.IO.File.OpenWrite(filePath);
                await image.CopyToAsync(stream);

                fileUrlPath = $"{uploadDir}/{fileName}";

                string fileExistName = categoryExist.ImagePath.Replace("uploads/", "");
                string imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, physicalPath, fileExistName);

                // remove image from server
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

            }


            string res = _CategoryService.CategoryUpdate(categoryDTO.categoryID, categoryDTO, fileUrlPath);
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
                    message = "Success Update Category"
                }
            );
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("/category")]
        public ActionResult DeleteCategory([FromBody] int Id)
        {
            string res = _CategoryService.CategoryDelete(Id);
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
/*
        [HttpGet("/CategoryLimit")]
        public ActionResult GetCategoryLimit()
        {
            return Ok(_CategoryService.GetCategoryLimit());
        }*/

        [Authorize(Roles = "admin")]
        [HttpPut("/category/Deactived")]
        public ActionResult DeactivateCategory([FromBody] int Id)
        {
            string res = _CategoryService.CategoryStatus(Id, false);
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
                    message = "Success Deactivate Category"
                }
            );
        }

        [Authorize(Roles = "admin")]
        [HttpPut("/category/Actived")]
        public ActionResult ActivateCategory([FromBody] int Id)
        {
            string res = _CategoryService.CategoryStatus(Id, true);
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
                    message = "Success Activate a Category"
                }
            );
        }

    }
}
