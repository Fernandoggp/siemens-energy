using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Project.Api.Configurations;
using Project.Application.Configurations;
using Project.Domain.Interfaces.Repositories;
using Project.Infrastructure.Persistence.Repositories;
using Project.Repository.Persistence;
using System.Text.Json;

namespace Project.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DbConnection")));

            services.AddScoped<IAutorRepository, AutorRepository>();

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

            services.AddHttpContextAccessor();

            services.AddDependencyInjectionConfiguration(Configuration);
            services.AddDependencyInjectionApplication();

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<HttpClient>();

            services.AddApiConfiguration(Configuration);
            services.AddVersioningConfiguration();
            services.AddSwaggerConfiguration();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            app.UseApiConfiguration(environment);

            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerConfiguration();
        }
    }
}