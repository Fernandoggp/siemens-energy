using Deviot.Common;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.UseCases;
using Project.Domain.Interfaces.UseCases.Autor;
using Project.Domain.Interfaces.Services.Autor;
using Project.Application.Services;
using Project.Application.UseCases.Autor;

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

            // Services - Autor
            services.AddScoped<IAutorService, AutorService>();

            return services;
        }
    }
}