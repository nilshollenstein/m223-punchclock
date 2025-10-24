using System.Net.Mime;
using M223PunchclockDotnet.Model;
using M223PunchclockDotnet.Service;
using Microsoft.AspNetCore.Mvc;

namespace M223PunchclockDotnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {

        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType<List<Category>>(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            var categorys =  await _categoryService.GetCategories();
            return Ok(categorys);
        } 
        
        [HttpGet]
        public async Task<ActionResult<List<Category>>> PostCategory()
        {
            return await _categoryService.GetCategories();
        }
    }
}
