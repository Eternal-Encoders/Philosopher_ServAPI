using Microsoft.AspNetCore.Mvc;
using Philosopher_ServAPI.Application;
using Philosopher_ServAPI.Core.Models.Entities.Book;

namespace Philosopher_ServAPI.Controllers
{
    [Route("textSection")]
    [ApiController]
    public class TextSectionController : ControllerBase
    {
        private readonly TextSectionService _textSectionService;

        public TextSectionController(TextSectionService textSectionService)
        {
            _textSectionService = textSectionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTextSectionById(string id)
        {
            if (id == null || id == "") return BadRequest("Empty input field");

            if (!Guid.TryParse(id, out Guid guid)) return BadRequest(
                "Specified ID is not valid");

            var textSection = await _textSectionService.GetTextSectionById(guid);
            return Ok(textSection);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTextSections()
        {
            var textSections = await _textSectionService.GetAllTextSections();
            return Ok(textSections);
        }

        [HttpPost("local")]
        public async Task<IActionResult> CreateTextSectionsFromLocal()
        {
            await _textSectionService.CreateTextSections();
            return Ok("Text sections are successfully created from local file");
        }
    }
}
