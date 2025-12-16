using Philosopher_ServAPI.Core.Models.Entities;
using Philosopher_ServAPI.Core.Models.Entities.Game;
using Philosopher_ServAPI.Core.Repositories;

namespace Philosopher_ServAPI.Infrastructure.Repositories
{
    public class CardRepository(SqlDbContext dBContext) : SqlRepository<Card>(dBContext), ICardRepository
    {
    }
}
