using AutoMapper;
using Philosopher_ServAPI.Core.Models.DTOs;
using Philosopher_ServAPI.Core.Models.Entities;
using Philosopher_ServAPI.Core.Repositories;
using Philosopher_ServAPI.Helpers.Exceptions;

namespace Philosopher_ServAPI.Application
{
    public class UserService
    {
        private readonly IUserRepository _repository;
        readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetUserDto> GetUserById(Guid id)
        {
            var user = await _repository.FirstOrDefaultAsync(u => u.Id == id) 
                ?? throw new NotFoundException($"User with id {id} is not found");

            var userDto = _mapper.Map<User,GetUserDto>(user);

            return userDto;
        }
    }
}
