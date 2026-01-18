using Microsoft.AspNetCore.Mvc;
using Philosopher_ServAPI.Application;
using Philosopher_ServAPI.Core.Models.DTOs.Game.Card;
using System;

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
        public async Task<IActionResult> GetCard(Guid? id)
        {
            //if (id == null || id == "") return BadRequest("Empty input field");
            if (id == null) return BadRequest("Id is not specified");

            //if (!Guid.TryParse(id, out Guid guid)) return BadRequest(
            //    "Specified ID is not valid");

            //var card = await _cardService.GetCardById(guid);
            var card = await _cardService.GetCardById((Guid)id);
            return Ok(card);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCards()
        {
            var cards = await _cardService.GetAllCards();
            return Ok(cards);
        }

        [HttpPost]
        public async Task<IActionResult> PostCard([FromBody] PostCardDto? cardDto)
        {
            if (cardDto == null) return BadRequest("Empty card body");

            var card = await _cardService.CreateCard(cardDto);
            return Ok(card);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCard(Guid? id)
        {
            if (id == null) return BadRequest("Id is not specified");

            await _cardService.DeleteCardById((Guid)id);
            return Ok("Card is deleted");
        }

        [HttpPatch]
        public async Task<IActionResult> PatchCard(
            [FromBody] UpdateCardDto? cardDto, Guid? id)
        {
            if (cardDto == null) return BadRequest("Empty card body");
            if (id == null) return BadRequest("Id is not specified");

            var card = await _cardService.UpdateCardById(cardDto, (Guid)id);
            return Ok(card);
        }
    }
}
