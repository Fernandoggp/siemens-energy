using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Project.Api.Configurations;
using Project.Application.Configurations;
using System.Data;
using Npgsql;
using Microsoft.Extensions.DependencyInjection;

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

            var signingConfigurations = new SigningConfigurations(Configuration);
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(Configuration.GetSection("JwtSettings")).Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);
            services.AddJwtSecurity(signingConfigurations, tokenConfigurations);

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.DictionaryKeyPolicy = null;
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

            services.AddHttpContextAccessor();

            // Dependency injection config
            services.AddDependencyInjectionConfiguration(Configuration);
            services.AddDependencyInjectionApplication(Configuration);
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<HttpClientWrapper>();
            services.AddSingleton<HttpClient>();

            // Api - Configuration
            services.AddApiConfiguration(Configuration);

            // Versionamento
            services.AddVersioningConfiguration();

            // Swagger config
            services.AddSwaggerConfiguration();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
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
