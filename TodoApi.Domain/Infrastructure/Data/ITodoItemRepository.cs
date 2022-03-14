using TodoApi.Domain.Models;

namespace TodoApi.Domain.Infrastructure.Data
{
    public interface ITodoItemRepository : IRepository<TodoItem, long>
    {
    }
}
