using AutoMapper;
using Philosopher_ServAPI.Core.Repositories;
using Philosopher_ServAPI.Helpers.Exceptions;
using System;

namespace Philosopher_ServAPI.Application
{
    public class LevelEndingService
    {
        readonly ILevelEndingRepository _levelEndingRep;
        readonly IMapper _mapper;

        public LevelEndingService(ILevelEndingRepository levelEndingRep,
            IMapper mapper)
        {
            _levelEndingRep = levelEndingRep;
            _mapper = mapper;
        }

        public async Task DeleteLevelEndingById(Guid id)
        {
            if (await _levelEndingRep.CountAsync(l => l.Id == id) == 0)
                throw new NotFoundException(
                    $"Level ending with ID {id} is not found");

            await _levelEndingRep.RemoveAsync(c => c.Id == id);
            await _levelEndingRep.SaveChanges();
        }
    }
}
