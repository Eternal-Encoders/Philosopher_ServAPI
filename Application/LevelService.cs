using AutoMapper;
using Philosopher_ServAPI.Core.Models.DTOs.Game.Card;
using Philosopher_ServAPI.Core.Models.DTOs.Game.Level;
using Philosopher_ServAPI.Core.Models.Entities.Game;
using Philosopher_ServAPI.Core.Repositories;
using Philosopher_ServAPI.Helpers.Exceptions;
using System;

namespace Philosopher_ServAPI.Application
{
    public class LevelService
    {
        private readonly ILevelRepository _levelRep;
        private readonly ILevelEndingRepository _levelEndingRep;
        private readonly ITextSectionRepository _textSectionRep;
        private readonly IMapper _mapper;

        public LevelService(ILevelRepository levelRep,
            IMapper mapper, ILevelEndingRepository levelEndingRep,
            ITextSectionRepository textSectionRep) 
        {
            _levelRep = levelRep;
            _levelEndingRep = levelEndingRep;
            _textSectionRep = textSectionRep;
            _mapper = mapper;
        }

        public async Task<Level> CreateLevel(PostLevelDto levelDto)
        {
            if (await _textSectionRep.CountAsync(ts => ts.Id == levelDto.TextSectionId) == 0)
                throw new NotFoundException($"Text section with id {levelDto.TextSectionId} is not found");

            Level level = _mapper.Map<PostLevelDto, Level>(levelDto);
            await _levelRep.AddAsync(level);

            LevelEnding levelEnding = new()
            {
                LevelId = level.Id,
                Name = "Default Ending",
                IsDefault = true
            };
            await _levelEndingRep.AddAsync(levelEnding);

            await _levelRep.SaveChanges();
            return level;
        }

        public async Task<Level> GetLevelById(Guid id)
        {
            var level = await _levelRep.FirstOrDefaultAsync(c => c.Id == id) ??
                throw new NotFoundException($"Level with id {id} is not found");

            return level;
        }

        public async Task<IReadOnlyList<Level>> GetAllLevels()
        {
            var levels = await _levelRep.ListAsync();

            return levels ?? [];
        }

        public async Task DeleteLevelById(Guid id)
        {
            if (await _levelRep.CountAsync(gp => gp.Id == id) == 0)
                throw new NotFoundException(
                    $"Level with ID {id} is not found");

            await _levelRep.RemoveAsync(c => c.Id == id);
            await _levelRep.SaveChanges();
        }

        public async Task<Level> UpdateLevelById(UpdateLevelDto levelDto, Guid id)
        {
            var level = await _levelRep.FirstOrDefaultAsync(_levelRep => _levelRep.Id == id) ??
                throw new NotFoundException($"Level with id {id} is not found");

            if (levelDto.TextSectionId is not null)
                if (await _textSectionRep.CountAsync(t => t.Id == levelDto.TextSectionId) == 1)
                    level.TextSectionId = levelDto.TextSectionId ??
                        throw new NotFoundException($"Text section with id {levelDto.TextSectionId} is not found");

            level.Name = levelDto.Name ?? level.Name;
            level.Description = levelDto.Description ?? level.Description;

            //await _levelRep.UpdateOneAsync(l => l.Id == id, level);
            await _levelRep.SaveChanges();
            //return level;
            return await _levelRep.FirstOrDefaultAsync(_levelRep => _levelRep.Id == id) ??
                throw new NotFoundException($"Level with id {id} is not found");
        }
    }
}
