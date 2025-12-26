using Philosopher_ServAPI.Core.Models.Entities.Game;
using Philosopher_ServAPI.Core.Shared;
using Philosopher_ServAPI.Core.Shared.Database;
using System.Linq.Expressions;

namespace Philosopher_ServAPI.Core.Repositories
{
    public interface IGameProgressRepository : IRepository<GameProgress>
    {
    }
}
