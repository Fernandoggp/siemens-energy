using Project.Application.Configurations;
using Project.Repository.Configurations;
using Project.Sql.Configurations;
using System.Diagnostics.CodeAnalysis;

namespace Project.Api.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionConfig
    {
        private static string SQL_SETTINGS = "ConnectionString";

        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Sql
            var sqlSettings = new DbConfig();
            configuration.Bind(SQL_SETTINGS, sqlSettings);
            services.AddSingleton(sqlSettings);

            // Dependency Injections
            services.AddDependencyInjectionApplication(configuration);

            // Dependency Injections Adapters
            services.AddDependencyInjection();

            return services;
        }
    }
}
