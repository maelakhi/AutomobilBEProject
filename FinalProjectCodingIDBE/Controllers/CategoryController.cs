using FinalProjectCodingIDBE.DTOs.CategoryDTO;
using FinalProjectCodingIDBE.Services;
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

        [HttpGet("/Category")]
        public ActionResult GetAll()
        {
            return Ok(_CategoryService.GetCategories());
        }

        [HttpGet("/category/{Id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(_CategoryService.GetByIdCategory(Id));
        }

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
