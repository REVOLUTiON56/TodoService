using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.Domain.Exceptions;
using TodoApi.Domain.Infrastructure.Data;
using TodoApi.Domain.Models;

namespace TodoApi.Domain.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository ?? throw new ArgumentNullException(nameof(todoItemRepository));
        }

        public Task<TodoItem> CreateAsync(TodoItem todoItem, CancellationToken token = default)
        {
            return _todoItemRepository.CreateAsync(todoItem, token);
        }

        public async Task<TodoItem> UpdateAsync(TodoItem todoItem, CancellationToken token = default)
        {
            var item = await _todoItemRepository.GetAsync(todoItem.Id, token);

            if (item == null)
            {
                throw new NotFoundException($"TodoItem with id {todoItem.Id} is not found.");
            }

            item.IsComplete = todoItem.IsComplete;
            item.Name = todoItem.Name;

            return await _todoItemRepository.UpdateAsync(item, token);
        }

        public Task DeleteAsync(long id, CancellationToken token = default)
        {
            return _todoItemRepository.DeleteAsync(id, token);
        }

        public Task<TodoItem> GetAsync(long id, CancellationToken token = default)
        {
            return _todoItemRepository.GetAsync(id, token);
        }

        public Task<List<TodoItem>> GetListAsync(CancellationToken token = default)
        {
            return _todoItemRepository.GetListAsync(token: token);
        }
    }
}
