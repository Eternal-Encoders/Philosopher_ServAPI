using Philosopher_ServAPI.Infrastructure.Repositories;

namespace Philosopher_ServAPI.Application
{
    public class UserService
    {
        private readonly UserRepository _repository;

        public UserService(UserRepository repository) 
        {
            _repository = repository;
        }

        public async UserDto GetUserById(Guid id)
        {

        }
    }
}
