using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Philosopher_ServAPI.Core.Shared.Database
{
    public interface IRepository<TAggregateRoot> : IReadOnlyRepository<TAggregateRoot> where TAggregateRoot : IAggregateRoot
    {
        Task AddAsync(TAggregateRoot aggregateRoot);
        Task AddRangeAsync(IReadOnlyList<TAggregateRoot> aggregateRoots);
        Task UpdateOneAsync(Expression<Func<TAggregateRoot, bool>> predicate, TAggregateRoot aggregateRoot);
        Task RemoveAsync(Expression<Func<TAggregateRoot, bool>> predicate);
        Task SaveChanges();
    }
}
