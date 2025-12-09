using Microsoft.AspNetCore.Mvc;
using Philosopher_ServAPI.Application;

namespace Philosopher_ServAPI.Controllers
{
    [Route("text")]
    [ApiController]
    public class TextController : ControllerBase
    {
        private readonly TextService _textService;

        public TextController(TextService textService)
        {
            _textService = textService;
        }

        [HttpGet("html/init")]
        public async Task<IActionResult> CreateHtmlText()
        {
            await _textService.CreateHtmlText();
            return Ok("File successfully created");
        }

        [HttpGet("html")]
        public async Task<IActionResult> GetHtmlText()
        {
            return Content(await _textService.GetHtmlText(), "text/html");
        }
    }
}
