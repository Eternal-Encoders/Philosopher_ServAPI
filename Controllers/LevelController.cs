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
        public async Task<IActionResult> GetLevelById(Guid? id)
        {
            if (id == null) return BadRequest("Id is not specified");

            var level = await _levelService.GetLevelById((Guid)id);
            return Ok(level);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllLevels()
        {
            var levels = await _levelService.GetAllLevels();
            return Ok(levels);
        }

        [HttpPost]
        public async Task<IActionResult> PostLevel([FromBody] PostLevelDto? levelDto)
        {
            if (levelDto == null) return BadRequest("Empty input field");

            var level = await _levelService.CreateLevel(levelDto);
            return Ok(level);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLevel(Guid? id)
        {
            if (id == null) return BadRequest("Id is not specified");

            await _levelService.DeleteLevelById((Guid)id);
            return Ok("Level was deleted");
        }

        [HttpPatch]
        public async Task<IActionResult> PatchLevel(
            [FromBody] UpdateLevelDto? levelDto, Guid? id)
        {
            if (levelDto == null) return BadRequest("Empty input field");
            if(id == null) return BadRequest("Id is not specified");

            var level = await _levelService.UpdateLevelById(levelDto, (Guid)id);
            return Ok(level);
        }
    }
}
