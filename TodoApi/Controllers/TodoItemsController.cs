using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.Domain.Services;
using TodoApi.Mapping;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/todoitems")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;

        public TodoItemsController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService ?? throw new ArgumentNullException(nameof(todoItemService));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TodoItemDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetListAsync(CancellationToken token = default)
        {
            var items = await _todoItemService.GetListAsync(token);

            return Ok(items.ToApiModel());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoItemDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(long id, CancellationToken token = default)
        {
            var item = await _todoItemService.GetAsync(id, token);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item.ToApiModel());
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoItemDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody] CreateOrUpdateTodoItemDto dto, CancellationToken token = default)
        {
            var item = dto.ToModel(id);
            var updatedItem = await _todoItemService.UpdateAsync(item, token);

            var response = updatedItem.ToApiModel();
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TodoItemDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOrUpdateTodoItemDto dto, CancellationToken token = default)
        {
            var item = dto.ToModel();
            var savedItem = await _todoItemService.CreateAsync(item, token);

            var response = savedItem.ToApiModel();
            return Created($"api/todoitems/{response.Id}", response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(long id, CancellationToken token = default)
        {
            await _todoItemService.DeleteAsync(id, token);

            return NoContent();
        }
    }
}
