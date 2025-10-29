using M223PunchclockDotnet.Model;
using M223PunchclockDotnet.Service;
using Microsoft.AspNetCore.Mvc;

namespace M223PunchclockDotnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagController :ControllerBase
    {
        private TagService _tagService;
        
        public TagController(TagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            return Ok(await _tagService.GetTagsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var category = await _tagService.GetTagByIdAsync(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Create(Tag tag)
        {
            var created = await _tagService.AddTagAsync(tag);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Tag tag)
        {
            var isUpdated = await _tagService.UpdateTagAsync(id, tag);
            if (!isUpdated)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _tagService.DeleteTagById(id);
            if (!isDeleted)
                return NotFound();
            return NoContent();
        }
    }
}
