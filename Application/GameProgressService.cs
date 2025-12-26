using AutoMapper;
using Philosopher_ServAPI.Core.Models.DTOs.Game.GameProgress;
using Philosopher_ServAPI.Core.Models.Entities.Game;
using Philosopher_ServAPI.Core.Repositories;
using Philosopher_ServAPI.Core.Shared.Database;
using Philosopher_ServAPI.Helpers.Exceptions;

namespace Philosopher_ServAPI.Application
{
    public class GameProgressService
    {
        readonly ILevelRepository _levelRepository;
        readonly ICardRepository _cardRepository;
        readonly IGameProgressRepository _gameProgressRepository;

        public GameProgressService(IGameProgressRepository gameProgressRepository, 
            ILevelRepository levelRepository,
            ICardRepository cardRepository)
        {
            _gameProgressRepository = gameProgressRepository;
            _levelRepository = levelRepository;
            _cardRepository = cardRepository;
        }

        public async Task<GameProgress> StartGame(Guid levelId)
        {
            if (await _levelRepository.CountAsync(l => l.Id == levelId) == 0)
                throw new NotFoundException($"Level with id {levelId} is not found");

            if (await _cardRepository.CountAsync(c => c.LevelId == levelId) == 0)
                throw new NotFoundException($"There are no cards in selected level");

            Guid cardId = (await _cardRepository.FirstOrDefaultAsync(c => 
                c.Number == 1 && c.LevelId == levelId))?.Id ?? 
                throw new NotFoundException($"Card with number 1 and level ID {levelId} is not found");

            GameProgress gameProgress = new GameProgress
            { 
                LevelId = levelId,
                CardId = cardId
            };

            await Task.Run(() => _gameProgressRepository.AddAsync(gameProgress))
                .ContinueWith(t => _gameProgressRepository.SaveChanges());

            return gameProgress;
        }

        public async Task<GameProgress> MakeMove(PostGameProgressDto move)
        {
            var progress = await _gameProgressRepository.FirstOrDefaultAsync(
                gp => gp.Id == move.Id) ?? throw new NotFoundException(
                    $"Game progress with id {move.Id} is not found");

            var card = await _cardRepository.FirstOrDefaultAsync(c =>
                c.Id == progress.CardId) ?? throw new NotFoundException(
                    $"Card with id {progress.CardId} is not found");

            var newCard = await _cardRepository.FirstOrDefaultAsync(c =>
                c.LevelId == progress.LevelId && c.Number == card.Number + 1) ?? 
                    throw new NotFoundException($"Next card with level ID {progress.LevelId} is not found");


            progress.StepNumber = newCard.Number;
            progress.CardId = newCard.Id;

            if (move.Choice == 1)
            {
                progress.Robotification = progress.Robotification + card.RobotDelta1;
                progress.Humanity = progress.Humanity + card.HumanDelta1;
            }
            else
            {
                progress.Robotification = progress.Robotification + card.RobotDelta2;
                progress.Humanity = progress.Humanity + card.HumanDelta2;
            }

            await _gameProgressRepository.SaveChanges();
            return progress;
        }
    }
}
