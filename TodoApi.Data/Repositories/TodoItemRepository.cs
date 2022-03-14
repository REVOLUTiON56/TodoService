using TodoApi.Domain.Infrastructure.Data;
using TodoApi.Domain.Models;

namespace TodoApi.Data.Repositories
{
    public class TodoItemRepository : EFRepository<TodoContext, TodoItem, long>, ITodoItemRepository
    {
        public TodoItemRepository(TodoContext context) : base(context)
        {
        }
    }
}
