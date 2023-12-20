﻿using FinalProjectCodingIDBE.DTOs.CategoryDTO;
using FinalProjectCodingIDBE.DTOs.ProductDTO;
using FinalProjectCodingIDBE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

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

        [Authorize]
        [HttpGet("/Category")]
        public ActionResult GetAll()
        {
            return Ok(_CategoryService.GetCategories());
        }

        [Authorize(Roles = "admin")]
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

            string fileUrlPath = $"https://localhost:7052/{uploadDir}/{fileName}";

            string res = _CategoryService.CategoryCreate(addCategoryDTO, fileUrlPath);

            if (!string.IsNullOrEmpty(res))
            {
                return BadRequest(res);
            }
            return Ok("Success Add Category");
        }

        [Authorize(Roles = "admin")]
        [HttpPut("/category")]
        public async Task<ActionResult> UpdatedCategory(int Id, [FromForm] AddCategoryDTO addCategoryDTO)
        {
            IFormFile image = addCategoryDTO.Image!;

            var extName = Path.GetExtension(image.FileName).ToLowerInvariant(); //.jpg

            string fileName = Guid.NewGuid().ToString() + extName;
            string uploadDir = "uploads";
            string physicalPath = $"wwwroot/{uploadDir}";

            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, physicalPath, fileName);

            using var stream = System.IO.File.OpenWrite(filePath);
            await image.CopyToAsync(stream);

            string fileUrlPath = $"https://localhost:7052/{uploadDir}/{fileName}";

            string res = _CategoryService.CategoryUpdate(Id, addCategoryDTO, fileUrlPath);

            if (!string.IsNullOrEmpty(res))
            {
                return BadRequest(res);
            }
            return Ok("Success Update Category");
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("/category/{Id}")]
        public ActionResult DeleteCategory(int Id)
        {
            string res = _CategoryService.CategoryDelete(Id);
            if (!string.IsNullOrEmpty(res))
            {
                return BadRequest(res);
            }
            return Ok("Successfull Delete");
        }
    }
}
