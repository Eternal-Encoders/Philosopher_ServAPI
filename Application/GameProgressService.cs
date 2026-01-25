using AutoMapper;
using Philosopher_ServAPI.Core.Models.DTOs.Game.GameProgress;
using Philosopher_ServAPI.Core.Models.Entities.Game;
using Philosopher_ServAPI.Core.Repositories;
using Philosopher_ServAPI.Helpers.Exceptions;
using System;

namespace Philosopher_ServAPI.Application
{
    public class GameProgressService
    {
        readonly ILevelRepository _levelRep;
        readonly ICardRepository _cardRep;
        readonly IGameProgressRepository _gameProgressRep;
        readonly ILevelEndingRepository _levelEndingRep;
        readonly IMapper _mapper;

        public GameProgressService(IGameProgressRepository gameProgressRep, 
            ILevelRepository levelRep,
            ICardRepository cardRep,
            ILevelEndingRepository levelEndingRep,
            IMapper mapper)
        {
            _gameProgressRep = gameProgressRep;
            _levelRep = levelRep;
            _cardRep = cardRep;
            _gameProgressRep = gameProgressRep;
            _levelEndingRep = levelEndingRep;
            _mapper = mapper;
        }

        public async Task<GetGameProgressDto> GetGameProgressById(Guid gameId)
        {
            var gameProgress = await _gameProgressRep.FirstOrDefaultJoinedAsync(g => g.Id == gameId) ??
                throw new NotFoundException($"Game progress not found");

            return _mapper.Map<GetGameProgressDto>(gameProgress);
        }

        public async Task<IReadOnlyList<GameProgress>> GetAllGameProgresses()
        {
            var gameProgresses = await _gameProgressRep.ListAsync();

            return gameProgresses;
        }

        public async Task<GetGameProgressDto> StartGame(Guid levelId)
        {
            if (await _levelRep.CountAsync(l => l.Id == levelId) == 0)
                throw new NotFoundException($"Level with id {levelId} is not found");

            if (await _cardRep.CountAsync(c => c.LevelId == levelId) == 0)
                throw new NotFoundException($"There are no cards in selected level");

            Card card = (await _cardRep.FirstOrDefaultAsync(c => 
                c.Number == 1 && c.LevelId == levelId)) ?? 
                throw new NotFoundException($"Card with number 1 and level ID {levelId} is not found");

            GameProgress gameProgress = new GameProgress
            { 
                LevelId = levelId,
                LastCardId = card.Id,
            };

            await _gameProgressRep.AddAsync(gameProgress);
            await _gameProgressRep.SaveChanges();

            var gameProgressDto = _mapper.Map<GetGameProgressDto>(gameProgress);
            gameProgressDto.LastCard = card;

            return gameProgressDto;
        }

        public async Task<GetGameProgressDto> MakeMove(Guid progressId, int choice)
        {
            var progress = await _gameProgressRep.FirstOrDefaultJoinedAsync(
                gp => gp.Id == progressId) ?? throw new NotFoundException(
                    $"Game progress with id {progressId} is not found");

            if (await _cardRep.CountAsync(c => c.Id == progress.LastCardId) == 0)
                throw new NotFoundException(
                    $"Card with id {progress.LastCardId} is not found");

            if (progress.LastCard is null)
                throw new Exception(
                    $"Card field is empty");

            if (progress.GameEnded)
            {
                var endedResult = _mapper.Map<GetGameProgressDto>(progress);
                return endedResult;
            }

            var newCard = await _cardRep.FirstOrDefaultAsync(c =>
                c.LevelId == progress.LevelId && c.Number == progress.StepNumber + 1);

            if (newCard is null)
            {
                var defaultEnding = await _levelEndingRep.FirstOrDefaultAsync(e =>
                    e.LevelId == progress.LevelId && e.IsDefault) ??
                    throw new NotFoundException(
                        $"Default level ending is not found");
                progress.GameEnded = true;

                progress.LevelEndingId = defaultEnding.Id;
                progress.LevelEnding = defaultEnding;
            }
            else
            {
                switch (choice)
                {
                    case 1:
                        progress.Robotification += progress.LastCard.RobotDelta1;
                        progress.Humanity += progress.LastCard.HumanDelta1;
                        break;

                    case 2:
                        progress.Robotification += progress.LastCard.RobotDelta2;
                        progress.Humanity += progress.LastCard.HumanDelta2;
                        break;

                    default:
                        throw new WrongInputException("Choice must be 1 or 2");
                }

                var normalEndings = await _levelEndingRep.ListAsync(e =>
                e.LevelId == progress.LevelId && !e.IsDefault);

                foreach (var ending in normalEndings)
                {
                    if (ending.RobotCondition is not null &&
                        (Math.Abs(progress.Robotification) + Math.Abs((int)ending.RobotCondition)) >=
                            (Math.Abs((int)ending.RobotCondition) * 2))
                    {
                        progress.GameEnded = true;
                        progress.LevelEndingId = ending.Id;
                        progress.LevelEnding = ending;
                        break;
                    }

                    if (ending.HumanCondition is not null &&
                        (Math.Abs(progress.Humanity) + Math.Abs((int)ending.HumanCondition)) >=
                            (Math.Abs((int)ending.HumanCondition) * 2))
                    {
                        progress.GameEnded = true;
                        progress.LevelEndingId = ending.Id;
                        progress.LevelEnding = ending;
                        break;
                    }
                }

                progress.StepNumber = newCard.Number;
                progress.LastCardId = newCard.Id;
                progress.LastCard = newCard;
            }

            await _gameProgressRep.SaveChanges();

            var result = _mapper.Map<GetGameProgressDto>(progress);
            return result;
        }

        public async Task DeleteGameProgressById(Guid progressId)
        {
            if (await _gameProgressRep.CountAsync(gp => gp.Id == progressId) == 0) 
                throw new NotFoundException(
                    $"Game progress with ID {progressId} is not found");

            await _gameProgressRep.RemoveAsync(c => c.Id == progressId);
            await _gameProgressRep.SaveChanges();
        }
    }
}
