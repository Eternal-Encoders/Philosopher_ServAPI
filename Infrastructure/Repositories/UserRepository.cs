using Philosopher_ServAPI.Core.Models.Entities;
using Philosopher_ServAPI.Core.Repositories;

namespace Philosopher_ServAPI.Infrastructure.Repositories
{
    public class UserRepository(SqlDbContext dBContext) : SqlRepository<User>(dBContext), IUserRepository
    {
    }
}
