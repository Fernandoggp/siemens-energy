using Microsoft.Extensions.DependencyInjection;
using Project.Domain.Interfaces.Repositories;
using Project.Infrastructure.Persistence.Repositories;
using Project.Repository.Repositories;

namespace Project.Repository.Configurations
{
    public static class DependencyInjectionRepository
    {
        public static IServiceCollection AddDependencyInjectionRepository(
            this IServiceCollection services)
        {
            services.AddScoped<IAutorRepository, AutorRepository>();
            services.AddScoped<IGeneroRepository, GeneroRepository>();
            services.AddScoped<ILivroRepository, LivroRepository>();

            return services;
        }
    }
}