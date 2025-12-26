using Microsoft.EntityFrameworkCore;
using Philosopher_ServAPI.Core.Models.Entities;
using Philosopher_ServAPI.Core.Models.Entities.Game;
using Philosopher_ServAPI.Core.Repositories;
using Philosopher_ServAPI.Core.Shared;
using System.Linq.Expressions;

namespace Philosopher_ServAPI.Infrastructure.Repositories
{
    public class CardRepository(SqlDbContext dbContext) : SqlRepository<Card>(dbContext), ICardRepository
    {
        public async Task<IReadOnlyList<Card>> ListOrderByNumber(Expression<Func<Card, bool>> predicate)
        {
            return await Items
                .AsNoTracking()
                .Where(predicate)
                .OrderBy(c => c.Number)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<int>> ListOfNumbers(Expression<Func<Card, bool>> predicate)
        {
            return await Items
                .AsNoTracking()
                .Where(predicate)
                .OrderBy(c => c.Number)
                .Select(c => c.Number)
                .ToListAsync();
        }
    }
}
