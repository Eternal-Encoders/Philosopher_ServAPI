using System.Linq.Expressions;

namespace Philosopher_ServAPI.Core.Shared.Database
{
    public interface IReadOnlyRepository<TAggregateRoot> where TAggregateRoot : IAggregateRoot
    {
        bool ReadOnly { get; }

        Task<TAggregateRoot> FirstAsync();
        Task<TAggregateRoot> FirstAsync(Expression<Func<TAggregateRoot, bool>> predicate);

        Task<TAggregateRoot?> FirstOrDefaultAsync();
        Task<TAggregateRoot?> FirstOrDefaultAsync(Expression<Func<TAggregateRoot, bool>> predicate);

        Task<IReadOnlyList<TAggregateRoot>> ListAsync();
        Task<IReadOnlyList<TAggregateRoot>> ListAsync(Expression<Func<TAggregateRoot, bool>> predicate);

        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<TAggregateRoot, bool>> predicate);
    }
}
