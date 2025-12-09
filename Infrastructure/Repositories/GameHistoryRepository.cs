using Philosopher_ServAPI.Core.Models.Entities;
using Philosopher_ServAPI.Core.Models.Entities.Game;
using Philosopher_ServAPI.Core.Repositories;

namespace Philosopher_ServAPI.Infrastructure.Repositories
{
    public class GameHistoryRepository(PostgresDBContext dBContext) : PostgresRepository<GameHistory>(dBContext), IGameHistoryRepository
    {
    }
}
