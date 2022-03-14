using System.Collections.Generic;
using System.Linq;
using TodoApi.Domain.Models;
using TodoApi.Models;

namespace TodoApi.Mapping
{
    public static class TodoItemsMapping
    {
        public static IEnumerable<TodoItemDTO> ToApiModel(this IEnumerable<TodoItem> models)
        {
            return models.Select(model => model.ToApiModel());
        }

        public static TodoItemDTO ToApiModel(this TodoItem model)
        {
            return new TodoItemDTO
            {
                Id = model.Id,
                IsComplete = model.IsComplete,
                Name = model.Name
            };
        }

        public static TodoItem ToModel(this CreateOrUpdateTodoItemDto apiModel, long id = default)
        {
            return new TodoItem
            {
                Id = id,
                IsComplete = apiModel.IsComplete,
                Name = apiModel.Name
            };
        }
    }
}
