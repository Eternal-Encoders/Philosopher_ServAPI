using Microsoft.AspNetCore.Mvc;
using Philosopher_ServAPI.Application;

namespace Philosopher_ServAPI.Controllers
{
    [Route("game")]
    [ApiController]
    public class GameProgressController : ControllerBase
    {
        private readonly GameProgressService _gameProgressService;

        public GameProgressController(GameProgressService gameProgressService)
        {
            _gameProgressService = gameProgressService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGameProgress(Guid? id)
        {
            if (id == null) return BadRequest("Id is not specified");

            var progress = await _gameProgressService.GetGameProgressById((Guid)id);
            return Ok(progress);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllGameProgresses()
        {
            return Ok(await _gameProgressService.GetAllGameProgresses());
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartGame(Guid? levelId)
        {
            if (levelId == null) return BadRequest("Level Id is not specified");

            var progress = await _gameProgressService.StartGame((Guid)levelId);
            return Ok(progress);
        }

        [HttpPatch("move")]
        public async Task<IActionResult> MakeMove(Guid? id, int choice)
        {
            if (id == null) return BadRequest("Id is not specified");

            var progress = await _gameProgressService.MakeMove((Guid)id, choice);
            return Ok(progress);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGameProgress(Guid? id)
        {
            if (id == null) return BadRequest("Id is not specified");

            await _gameProgressService.DeleteGameProgressById((Guid)id);
            return Ok("Game progress was deleted");
        }
    }
}
