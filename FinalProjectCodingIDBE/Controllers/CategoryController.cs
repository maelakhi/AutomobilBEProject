using FinalProjectCodingIDBE.DTOs.CategoryDTO;
using FinalProjectCodingIDBE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectCodingIDBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorysController : ControllerBase
    {
        private readonly CategoryService _CategoryService;
        public CategorysController(CategoryService serviceCategorys)
        {
            _CategoryService = serviceCategorys;
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
        public ActionResult CreateCategory([FromForm] AddCategoryDTO addCategoryDTO)
        {
            string res = _CategoryService.CategoryCreate(addCategoryDTO);

            if (!string.IsNullOrEmpty(res))
            {
                return BadRequest(res);
            }
            return Ok("Success Add Category");
        }

        [Authorize(Roles = "admin")]
        [HttpPut("/category")]
        public ActionResult UpdatedCategory(int Id, [FromForm] AddCategoryDTO addCategoryDTO)
        {
            string res = _CategoryService.CategoryUpdate(Id, addCategoryDTO);
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
