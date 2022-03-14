namespace TodoApi.Models
{
    public class CreateOrUpdateTodoItemDto
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
