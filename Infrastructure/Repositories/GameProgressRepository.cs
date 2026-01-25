using Microsoft.EntityFrameworkCore;
using Philosopher_ServAPI.Core.Models.DTOs.Game.GameProgress;
using Philosopher_ServAPI.Core.Models.Entities;
using Philosopher_ServAPI.Core.Models.Entities.Game;
using Philosopher_ServAPI.Core.Repositories;
using Philosopher_ServAPI.Core.Shared;
using System.Linq.Expressions;

namespace Philosopher_ServAPI.Infrastructure.Repositories
{
    public class GameProgressRepository(SqlDbContext dBContext) : SqlRepository<GameProgress>(dBContext), IGameProgressRepository
    {
        public async Task<GameProgress?> FirstOrDefaultJoinedAsync(Expression<Func<GameProgress, bool>> predicate)
        {
            var res = await dBContext.GameProgresses
                .Include(x => x.LastCard)
                .Include(x => x.LevelEnding)
                .FirstOrDefaultAsync(predicate);

            return res;
        }
    }
}
