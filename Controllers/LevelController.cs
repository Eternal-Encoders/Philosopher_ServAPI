using Microsoft.AspNetCore.Mvc;
using Philosopher_ServAPI.Application;
using Philosopher_ServAPI.Core.Models.DTOs.Game.Level;

namespace Philosopher_ServAPI.Controllers
{
    [Route("level")]
    [ApiController]
    public class LevelController : ControllerBase
    {
        private readonly LevelService _levelService;

        public LevelController(LevelService levelService)
        {
            _levelService = levelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLevelById(string id)
        {
            if (id == null || id == "") return BadRequest("Empty input field");

            if (!Guid.TryParse(id, out Guid guid)) return BadRequest(
                "Specified ID is not valid");

            var level = await _levelService.GetLevelById(guid);
            return Ok(level);
        }

        [HttpPost]
        public async Task<IActionResult> PostLevel([FromBody] PostLevelDto? levelDto)
        {
            if (levelDto == null) return BadRequest("Empty input field");

            await _levelService.CreateLevel(levelDto);
            return Ok("Successfully created");
        }
    }
}
