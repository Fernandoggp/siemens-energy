using Microsoft.Extensions.DependencyInjection;
using Project.Domain.Interfaces.Repositories.Autor;
using Project.Infrastructure.Persistence.Repositories;

namespace Project.Repository.Configurations
{
    public static class DependencyInjectionRepository
    {
        public static IServiceCollection AddDependencyInjectionRepository(
            this IServiceCollection services)
        {
            services.AddScoped<IAutorRepository, AutorRepository>();

            return services;
        }
    }
}