using Microsoft.Extensions.DependencyInjection;
using TodoApi.Domain.Services;

namespace TodoApi.Domain
{
    public static class ContainerRegistrations
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ITodoItemService, TodoItemService>();

            return services;
        }
    }
}
