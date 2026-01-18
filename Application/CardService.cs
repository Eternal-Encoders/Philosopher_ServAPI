using AutoMapper;
using Philosopher_ServAPI.Core.Models.DTOs.Game.Card;
using Philosopher_ServAPI.Core.Models.Entities.Game;
using Philosopher_ServAPI.Core.Repositories;
using Philosopher_ServAPI.Helpers.Exceptions;

namespace Philosopher_ServAPI.Application
{
    public class CardService
    {
        readonly ICardRepository _cardRep;
        readonly ILevelRepository _levelRep;
        readonly IMapper _mapper;

        public CardService(ICardRepository cardRep, IMapper mapper,
            ILevelRepository levelRep)
        {
            _cardRep = cardRep;
            _levelRep = levelRep;
            _mapper = mapper;
        }

        public async Task<Card> CreateCard(PostCardDto cardDto)
        {
            if (cardDto.Number - (await _cardRep.CountAsync(c => c.LevelId == cardDto.LevelId)) > 1)
                throw new WrongInputException("Card number is higher than cards count");

            if (await _cardRep.CountAsync(c => c.Number == cardDto.Number &&
                c.LevelId == cardDto.LevelId) > 0)
            {
                var lateCards = await _cardRep.ListAsync(c => c.Number > cardDto.Number &&
                    c.LevelId == cardDto.LevelId);

                if (lateCards.Count != 0)
                {
                    foreach (var lateCard in lateCards)
                    {
                        lateCard.Number++;
                    }
                }
            }

            Card card = _mapper.Map<PostCardDto, Card>(cardDto);
            await _cardRep.AddAsync(card);
            await _cardRep.SaveChanges();

            return card;
        }

        public async Task CreateManyCards(PostCardDto[] cardDtos)
        {
            foreach (var cardDto in cardDtos)
            {
                if (cardDto.Number - (await _cardRep.CountAsync(c => c.LevelId == cardDto.LevelId)) > 1)
                    throw new WrongInputException("Card number is higher than cards count");

                if (await _cardRep.CountAsync(c => c.Number == cardDto.Number &&
                    c.LevelId == cardDto.LevelId) > 0)
                {
                    throw new AlreadyExistsException(
                        $"Card with number {cardDto.Number} and level ID {cardDto.LevelId} already exists");
                }
            }

            Card[] cards = _mapper.Map<PostCardDto[], Card[]>(cardDtos);

            await _cardRep.AddRangeAsync(cards);
            await _cardRep.SaveChanges();
        }

        public async Task<Card> GetCardById(Guid id)
        {
            var card = await _cardRep.FirstOrDefaultAsync(c => c.Id == id) ??
                throw new NotFoundException($"Card with id {id} is not found");

            return card;
        }

        public async Task<IReadOnlyList<Card>> GetAllCards()
        {
            var cards = await _cardRep.ListAsync();

            return cards ?? [];
        }

        public async Task DeleteCardById(Guid id)
        {
            if (await _cardRep.CountAsync(c => c.Id == id) < 1)
                throw new NotFoundException($"Card with id {id} is not found");

            await _cardRep.RemoveAsync(c => c.Id == id);
            await _cardRep.SaveChanges();
        }

        public async Task<Card> UpdateCardById(UpdateCardDto cardDto, Guid id)
        {
            var card = await _cardRep.FirstOrDefaultAsync(c => c.Id == id) ??
                throw new NotFoundException($"Card with id {id} is not found");

            var number = cardDto.Number ?? card.Number;
            var levelId = cardDto.LevelId ?? card.LevelId;

            var twistCard = await _cardRep.FirstOrDefaultAsync(
                        c => c.Number == number && c.LevelId == levelId) ??
                        throw new NotFoundException(
                            $"Card with number {number} and " +
                            $"level ID {levelId} for twist is not found");

            twistCard.LevelId = levelId;
            twistCard.Number = number;

            if (cardDto.LevelId.HasValue)
                if (await _levelRep.CountAsync(l => l.Id == cardDto.LevelId) == 1)
                    card.LevelId = cardDto.LevelId ?? card.LevelId;

            card.Number = cardDto.Number ?? card.Number;
            card.Text = cardDto.Text ?? card.Text;
            card.Name = cardDto.Name ?? card.Name;
            card.FirstChoiceText = cardDto.FirstChoiceText ?? card.FirstChoiceText;
            card.SecondChoiceText = cardDto.SecondChoiceText ?? card.SecondChoiceText;
            card.HumanDelta1 = cardDto.HumanDelta1 ?? card.HumanDelta1;
            card.HumanDelta2 = cardDto.HumanDelta2 ?? card.HumanDelta2;
            card.RobotDelta1 = cardDto.RobotDelta1 ?? card.RobotDelta1;
            card.RobotDelta2 = cardDto.RobotDelta2 ?? card.RobotDelta2;
            card.ImageLink = cardDto.ImageLink ?? card.ImageLink;

            await _cardRep.SaveChanges();
            return card;
        }

        //public Task<Card> GetRandomCard()
        //{

        //}

        public async Task CheckCardsNumbers(Guid levelId)
        {
            int counter = 1;
            if (await _cardRep.CountAsync(c => c.LevelId == levelId) > 0)
            {
                foreach (var number in await _cardRep.ListOfNumbers(
                    c => c.LevelId == levelId))
                {
                    if (number != counter)
                        throw new WrongInputException(
                            $"Missing card with number {counter} and level ID {levelId}");

                    counter++;
                }
            }
        }
    }
}
