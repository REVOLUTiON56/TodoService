namespace TodoApi.Domain.Models
{
    public class TodoItem : BaseEntity<long>
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Secret { get; set; }
    }
}
