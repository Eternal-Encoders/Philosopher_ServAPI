using Philosopher_ServAPI.Core.Models.Entities;
using Philosopher_ServAPI.Core.Models.Entities.Game;
using Philosopher_ServAPI.Core.Repositories;
using Philosopher_ServAPI.Core.Shared;
using System.Linq.Expressions;

namespace Philosopher_ServAPI.Infrastructure.Repositories
{
    public class GameProgressRepository(SqlDbContext dBContext) : SqlRepository<GameProgress>(dBContext), IGameProgressRepository
    {
    }
}
