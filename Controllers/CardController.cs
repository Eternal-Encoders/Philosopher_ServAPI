using Microsoft.AspNetCore.Mvc;
using Philosopher_ServAPI.Application;
using Philosopher_ServAPI.Core.Models.DTOs.Game.Card;

namespace Philosopher_ServAPI.Controllers
{
    [Route("card")]
    [ApiController]
    public class CardController: ControllerBase
    {
        private readonly CardService _cardService;

        public CardController(CardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCard(string? id)
        {
            if (id == null || id == "") return BadRequest("Empty input field");

            if (!Guid.TryParse(id, out Guid guid)) return BadRequest(
                "Specified ID is not valid");

            var card = await _cardService.GetCardById(guid);
            return Ok(card);
        }

        [HttpPost]
        public async Task<IActionResult> PostCard([FromBody] PostCardDto? cardDto)
        {
            if (cardDto == null) return BadRequest("Empty input field");

            await _cardService.CreateCard(cardDto);
            return Ok("Successfully created");
        }
    }
}
