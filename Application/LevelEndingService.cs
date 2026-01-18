using AutoMapper;
using Philosopher_ServAPI.Core.Repositories;

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
            await _levelEndingRep.RemoveAsync(c => c.Id == id);
            await _levelEndingRep.SaveChanges();
        }
    }
}
