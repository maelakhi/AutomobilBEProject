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

        [HttpGet("/Categorys")]   
        public ActionResult GetAll()
        {
            return Ok(_CategoryService.GetCategories());
        }
    }
}
