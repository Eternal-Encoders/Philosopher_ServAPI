using Microsoft.EntityFrameworkCore;
using Philosopher_ServAPI.Core.Shared;
using Philosopher_ServAPI.Core.Shared.Database;
using System;
using System.Linq.Expressions;

namespace Philosopher_ServAPI.Infrastructure
{
    public class SqlRepository<TAggregateRoot> : IRepository<TAggregateRoot> where TAggregateRoot : class, 
        IAggregateRoot
    {
        public bool ReadOnly { get; }

        private readonly DbContext _dBContext;
        private DbSet<TAggregateRoot> Items => _dBContext.Set<TAggregateRoot>();
        //protected virtual IQueryable<TAggregateRoot> Items => ReadOnly ? _items.AsNoTracking() : _items;

        public SqlRepository(DbContext dBContext, bool readOnly = false)
        {
            _dBContext = dBContext;
            ReadOnly = readOnly;
        }

        ////

        public async Task AddAsync(TAggregateRoot aggregateRoot)
        {
            if (!ReadOnly)
            {
                await Items.AddAsync(aggregateRoot);
            }
        }

        public async Task AddRangeAsync(IReadOnlyList<TAggregateRoot> aggregateRoots)
        {
            if (!ReadOnly)
            {
                await Items.AddRangeAsync(aggregateRoots);
            }
        }

        public async Task RemoveAsync(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            if (!ReadOnly)
            {
                var entities = await Items.Where(predicate).ToListAsync();
                if (entities != null)
                    Items.RemoveRange(entities);
            }
        }

        public async Task UpdateOneAsync(Expression<Func<TAggregateRoot, bool>> predicate, TAggregateRoot aggregateRoot)
        {
            if (!ReadOnly)
            {
                var entityForEdit = await Items.Where(predicate).FirstOrDefaultAsync() 
                    ?? throw new Exception("Not Found");

                entityForEdit = aggregateRoot ?? entityForEdit;
                Items.Update(entityForEdit);
            }
        }

        public async Task SaveChanges()
        {
            if (!ReadOnly)
            {
                await _dBContext.SaveChangesAsync();
            }
        }

        ////

        public async Task<int> CountAsync()
        {
            return await Items
                .AsNoTracking()
                .CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            return await Items
                .AsNoTracking()
                .CountAsync(predicate);
        }

        public async Task<TAggregateRoot> FirstAsync()
        {
            return await Items
                .AsNoTracking()
                .FirstAsync();
        }

        public async Task<TAggregateRoot> FirstAsync(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            return await Items
                .AsNoTracking()
                .FirstAsync(predicate);
        }

        public async Task<TAggregateRoot?> FirstOrDefaultAsync()
        {
            return await Items
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<TAggregateRoot?> FirstOrDefaultAsync(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            return await Items
                .AsNoTracking()
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<IReadOnlyList<TAggregateRoot>> ListAsync()
        {
            return await Items
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<TAggregateRoot>> ListAsync(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            return await Items
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }
    }
}
