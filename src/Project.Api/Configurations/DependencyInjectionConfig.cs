using Project.Application.Configurations;
using Project.Repository.Configurations;
using System.Diagnostics.CodeAnalysis;

namespace Project.Api.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDependencyInjectionApplication();
            services.AddDependencyInjectionRepository();

            return services;
        }
    }
}