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
        public async Task<IActionResult> GetTextSectionById(Guid? id)
        {
            if (id == null) return BadRequest("Id is not specified");

            var textSection = await _textSectionService.GetTextSectionById((Guid)id);
            return Ok(textSection);
        }

        [HttpGet("all")]
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
