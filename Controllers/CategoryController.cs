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
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType<Category>(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            return Ok(await _categoryService.GetCategoriesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType<Category>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Category>> Create(Category category)
        {
            var created = await _categoryService.AddCategoryAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, Category category)
        {
            var isUpdated = await _categoryService.UpdateCategoryAsync(id, category);
            if (!isUpdated)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _categoryService.DeleteCategoryAsync(id);
            if (!isDeleted)
                return NotFound();
            return NoContent();
        }
    }
}
