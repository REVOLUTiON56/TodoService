using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApi.Domain.Infrastructure.Data
{
    public interface IRepository<T>
    {
        Task<T> CreateAsync(T entity, CancellationToken token = default);
        Task<T> UpdateAsync(T entity, CancellationToken token = default);
        Task DeleteAsync(T entity, CancellationToken token = default);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter = null, CancellationToken token = default);
    }

    public interface IRepository<T, in TKey> : IRepository<T>
    {
        Task DeleteAsync(TKey id, CancellationToken token = default);
        Task<T> GetAsync(TKey id, CancellationToken token = default);
        Task<bool> IsExistsAsync(TKey id, CancellationToken token = default);
    }
}
