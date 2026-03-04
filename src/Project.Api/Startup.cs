using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Project.Api.Configurations;
using Project.Application.Configurations;
using Project.Domain.Interfaces.Repositories.Autor;
using Project.Infrastructure.Persistence.Repositories;
using Project.Repository.Persistence;

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
                options.UseNpgsql(
                    Configuration["ConnectionStrings:DefaultConnection"]
                ));

            services.AddScoped<IAutorRepository, AutorRepository>();

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.DictionaryKeyPolicy = null;
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
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
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerConfiguration();
            app.UseApiConfiguration(environment);
        }
    }
}