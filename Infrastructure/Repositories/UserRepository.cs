using Philosopher_ServAPI.Core.Models.Entities;
using Philosopher_ServAPI.Core.Repositories;

namespace Philosopher_ServAPI.Infrastructure.Repositories
{
    public class UserRepository(PostgresDBContext dBContext) : PostgresRepository<User>(dBContext), IUserRepository
    {
    }
}
