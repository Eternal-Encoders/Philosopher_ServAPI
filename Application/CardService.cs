using AutoMapper;
using Philosopher_ServAPI.Core.Models.DTOs.Game.Card;
using Philosopher_ServAPI.Core.Models.Entities.Book;
using Philosopher_ServAPI.Core.Models.Entities.Game;
using Philosopher_ServAPI.Core.Repositories;
using Philosopher_ServAPI.Helpers.Exceptions;

namespace Philosopher_ServAPI.Application
{
    public class CardService
    {
        readonly ICardRepository _repository;
        readonly IMapper _mapper;

        public CardService(ICardRepository repository, IMapper mapper) 
        { 
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateCard(PostCardDto cardDto)
        {
            Card card = _mapper.Map<PostCardDto, Card>(cardDto);
            int counter = 1;

            if (await _repository.CountAsync(c => c.Number == cardDto.Number &&
                c.LevelId == cardDto.LevelId) > 0)
            {
                //throw new AlreadyExistsException(
                //    $"Card with number {cardDto.Number} and level ID {cardDto.LevelId} already exists");
                var lateCards = await _repository.ListAsync(c => c.Number > cardDto.Number);

                if (lateCards.Count != 0)
                {
                    foreach (var lateCard in lateCards)
                    {
                        lateCard.Number++;
                    }
                }
            }

            await _repository.AddAsync(card);

            if (await _repository.CountAsync(c => c.LevelId == cardDto.LevelId) > 0)
            {
                foreach (var number in await _repository.ListOfNumbers(
                    c => c.LevelId == cardDto.LevelId))
                {
                    if (number != counter)
                        throw new WrongInputException(
                            $"Missing card with number {counter} and level ID {cardDto.LevelId}");

                    counter++;
                }
            }

            await _repository.SaveChanges();

            //Альтернатива:
            //await _repository.AddAsync(card);
            //await _repository.SaveChanges();
        }

        public async Task<Card> GetCardById(Guid id)
        {
            var card = await _repository.FirstOrDefaultAsync(c => c.Id == id) ??
                throw new NotFoundException($"Card with id {id} is not found");

            return card;
        }

        public async Task<IReadOnlyList<Card>> GetAllCards()
        {
            var cards = await _repository.ListAsync();

            return cards ?? [];
        }

        //public Task<Card> GetRandomCard()
        //{

        //}
    }
}
