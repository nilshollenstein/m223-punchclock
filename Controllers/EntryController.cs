using System.Net.Mime;
using M223PunchclockDotnet.Model;
using M223PunchclockDotnet.Service;
using Microsoft.AspNetCore.Mvc;

namespace M223PunchclockDotnet.Controllers
{
    [ApiController]
    [Route("entry")]
    public class EntryController : ControllerBase
    {
        private readonly IEntryService _entryService;

        public EntryController(IEntryService entryService)
        {
            _entryService = entryService;
        }

        [HttpGet]
        [ProducesResponseType<Entry>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var allEntries = await _entryService.FindAll();
            return Ok(allEntries);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType<Entry>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var entry = await _entryService.GetEntryById(id);
            if(entry == null) 
                return NotFound();
            return Ok(entry);
        }

        [HttpPost()]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType<Entry>(StatusCodes.Status201Created)]
        public async Task<ActionResult<Entry>> AddEntry([FromBody] Entry entry){
            var newElement = await _entryService.AddEntry(entry);

            return CreatedAtAction(nameof(Get), new{id = entry.Id}, entry);
        }

        [HttpDelete()]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType<Entry>(StatusCodes.Status200OK)]
        public async Task<ActionResult<Entry>> DeleteEntry([FromBody] Entry entry) {
            var deletedEntry = await _entryService.DeleteEntry(entry);
            return Ok(deletedEntry) ;
        } 
        
        [HttpPut()]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType<Entry>(StatusCodes.Status200OK)]
        public async Task<ActionResult<Entry>> UpdateEntry([FromBody] Entry entry) {
            var updatedElement = await _entryService.UpdateEntry(entry);
            return Ok(updatedElement) ;
        }


    }
}
