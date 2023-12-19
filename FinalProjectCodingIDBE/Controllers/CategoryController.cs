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
        public CategorysController(CategoryService serviceCategorys) { 
            _CategoryService = serviceCategorys;
        }

        [HttpGet("/Category")]   
        public ActionResult GetAll()
        {
            return Ok(_CategoryService.GetCategories());
        }

        [HttpGet("/categoryLimit")]
        public ActionResult GetLimit()
        {
            return Ok(_CategoryService.GetLimitCategory());
        }

        [HttpGet("/category/{Id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(_CategoryService.GetByIdCategory(Id));
        }

        [HttpPost("/category")]
        public ActionResult CreateCategory([FromBody] AddCategoryDTO addCategoryDTO)
        {
            return Ok(_CategoryService.CategoryCreate(addCategoryDTO));
        }
        [HttpPut("/category")]
        public ActionResult UpdatedCategory(int Id, [FromBody] AddCategoryDTO addCategoryDTO)
        {
            return Ok(_CategoryService.CategoryUpdate(Id, addCategoryDTO));
        }
        [HttpDelete("/category/{Id}")]
        public ActionResult DeleteCategory(int Id)
        {
            return Ok(_CategoryService.CategoryDelete(Id));
        }

    }
}
