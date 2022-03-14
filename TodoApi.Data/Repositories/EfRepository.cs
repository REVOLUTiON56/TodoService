using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.Domain.Infrastructure.Data;
using TodoApi.Domain.Models;

namespace TodoApi.Data.Repositories
{
    /// <summary>
    /// Simple implementation of base EF Repository.
    /// </summary>
    public abstract class EFRepository<TContext, TEntity, TKey> : BaseEFRepository<TContext, TEntity>, IRepository<TEntity, TKey>
        where TContext : DbContext
        where TEntity : BaseEntity<TKey>, new()
    {
        protected EFRepository(TContext context) : base(context)
        {

        }

        public virtual Task<TEntity> GetAsync(TKey id, CancellationToken token = default)
        {
            return Table.FindAsync(new object[] { id }, token).AsTask();
        }

        public virtual Task<bool> IsExistsAsync(TKey id, CancellationToken token = default)
        {
            return Table.AnyAsync(x => x.Equals(id), token);
        }

        public virtual Task DeleteAsync(TKey id, CancellationToken token = default)
        {
            var entity = new TEntity { Id = id };
            return DeleteAsync(entity, token);
        }
    }
}
