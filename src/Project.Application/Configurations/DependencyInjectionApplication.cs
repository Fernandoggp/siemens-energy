using Deviot.Common;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.UseCases;
using Project.Domain.Interfaces.UseCases.Autor;
using Project.Application.Services;
using Project.Application.UseCases.Autor;
using Project.Application.UseCases.Genero;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases.Genero;

namespace Project.Application.Configurations
{
    public static class DependencyInjectionApplication
    {
        public static IServiceCollection AddDependencyInjectionApplication(
            this IServiceCollection services)
        {
            // Common
            services.AddScoped<INotifier, Notifier>();

            // Use Cases - Autor
            services.AddScoped<ICreateAutorUseCase, CreateAutorUseCase>();
            services.AddScoped<IGetAllAutoresUseCase, GetAllAutoresUseCase>();
            services.AddScoped<IUpdateAutorUseCase,  UpdateAutorUseCase>();
            services.AddScoped<IDeleteAutorByIdUseCase, DeleteAutorByIdUseCase>();

            // Use Cases - Genero
            services.AddScoped<ICreateGeneroUseCase, CreateGeneroUseCase>();
            services.AddScoped<IGetAllGenerosUseCase, GetAllGenerosUseCase>();
            services.AddScoped<IUpdateGeneroUseCase, UpdateGeneroUseCase>();
            services.AddScoped<IDeleteGeneroByIdUseCase, DeleteGeneroByIdUseCase>();

            // Services
            services.AddScoped<IAutorService, AutorService>();
            services.AddScoped<IGeneroService, GeneroService>();

            return services;
        }
    }
}