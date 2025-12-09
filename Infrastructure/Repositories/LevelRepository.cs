using Philosopher_ServAPI.Core.Models.Entities.Game;
using Philosopher_ServAPI.Core.Repositories;

namespace Philosopher_ServAPI.Infrastructure.Repositories
{
    public class LevelRepository(PostgresDBContext dBContext) : PostgresRepository<Level>(dBContext), ILevelRepository
    {
    }
}
