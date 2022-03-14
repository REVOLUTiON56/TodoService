using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.Domain.Infrastructure.Data;

namespace TodoApi.Data.Repositories
{
    /// <summary>
    /// Simple implementation of base EF Repository.
    /// </summary>
    public abstract class BaseEFRepository<TContext, TEntity> : IRepository<TEntity>
        where TContext : DbContext
        where TEntity : class, new()
    {
        protected readonly DbSet<TEntity> Table;
        protected readonly TContext Context;

        protected BaseEFRepository(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Table = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken token = default)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Table.Add(entity);
            await Context.SaveChangesAsync(token);

            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token = default)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Table.Update(entity);
            await Context.SaveChangesAsync(token);

            return entity;
        }

        public virtual async Task DeleteAsync(TEntity entity, CancellationToken token = default)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Table.Remove(entity);
            await Context.SaveChangesAsync(token);
        }

        public virtual Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null, CancellationToken token = default)
        {
            var query = Table.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToListAsync(token);
        }
    }
}
