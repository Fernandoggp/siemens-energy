using Microsoft.Extensions.DependencyInjection;
using Project.Domain.Interfaces.Repositories;
using Project.Repository.Builders.Portfolio;
using Project.Repository.Builders.Sector;
using Project.Repository.Builders.User;
using Project.Repository.Core;
using Project.Repository.Repositories;

namespace Project.Sql.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            // Repositories
            services.AddSingleton<ISectorRepository, SectorRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IPortfolioRepository, PortfolioRepository>();

            // Builders
            services.AddSingleton<ISectorBuilder, SectorBuilder>();
            services.AddSingleton<IUserBuilder, UserBuilder>();
            services.AddSingleton<IPortfolioBuilder, PortfolioBuilder>();

            // Services
            services.AddSingleton<IDbService, DbService>();
            services.AddSingleton<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
