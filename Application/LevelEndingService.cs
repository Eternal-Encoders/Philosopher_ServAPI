using AutoMapper;
using Philosopher_ServAPI.Core.Repositories;

namespace Philosopher_ServAPI.Application
{
    public class LevelEndingService
    {
        readonly ILevelEndingRepository _repository;
        readonly IMapper _mapper;

        public LevelEndingService(ILevelEndingRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
