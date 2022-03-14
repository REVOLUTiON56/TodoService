using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.Domain.Models;

namespace TodoApi.Domain.Services
{
    public interface ITodoItemService
    {
        Task<TodoItem> CreateAsync(TodoItem todoItem, CancellationToken token = default);
        Task<TodoItem> UpdateAsync(TodoItem todoItem, CancellationToken token = default);
        Task DeleteAsync(long id, CancellationToken token = default);
        Task<TodoItem> GetAsync(long id, CancellationToken token = default);
        Task<List<TodoItem>> GetListAsync(CancellationToken token = default);
    }
}
