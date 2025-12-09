using AutoMapper;
using Philosopher_ServAPI.Core.Models.DTOs.Game.Card;
using Philosopher_ServAPI.Core.Models.DTOs.Game.Level;
using Philosopher_ServAPI.Core.Models.Entities.Game;
using Philosopher_ServAPI.Core.Repositories;
using Philosopher_ServAPI.Helpers.Exceptions;

namespace Philosopher_ServAPI.Application
{
    public class LevelService
    {
        private readonly ILevelRepository _repository;
        private readonly IMapper _mapper;

        public LevelService(ILevelRepository repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateLevel(PostLevelDto levelDto)
        {
            Level level = _mapper.Map<PostLevelDto, Level>(levelDto);

            await Task.Run(() => _repository.AddAsync(level))
                .ContinueWith(t => _repository.SaveChanges());
        }

        public async Task<Level> GetLevelById(Guid id)
        {
            var level = await _repository.FirstOrDefaultAsync(c => c.Id == id) ??
                throw new NotFoundException($"Level with id {id} is not found");

            return level;
        }

        public async Task<IReadOnlyList<Level>> GetAllLevels()
        {
            var levels = await _repository.ListAsync();

            return levels ?? [];
        }
    }
}
