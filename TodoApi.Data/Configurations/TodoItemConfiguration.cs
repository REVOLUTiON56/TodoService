using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApi.Domain.Models;

namespace TodoApi.Data.Configurations
{
    internal class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(e => e.Name)
                .IsRequired();



            // other rules if needed

            builder.ToTable("TodoItems");
        }
    }
}
