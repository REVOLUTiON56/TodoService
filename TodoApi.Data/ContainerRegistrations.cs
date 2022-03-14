using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.Data.Repositories;
using TodoApi.Domain.Infrastructure.Data;

namespace TodoApi.Data
{
    public static class ContainerRegistrations
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, string connection)
        {
            services.AddDbContext<TodoContext>(options =>
            {
                options.UseSqlServer(connection);
                options.EnableDetailedErrors();
            });

            services.AddScoped<ITodoItemRepository, TodoItemRepository>();

            return services;
        }
    }
}
