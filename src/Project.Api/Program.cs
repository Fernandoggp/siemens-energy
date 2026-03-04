using Project.Api;
using Serilog;
using Serilog.Sinks.Grafana.Loki;
using Serilog.Sinks.SystemConsole.Themes;
using System.Diagnostics.CodeAnalysis;

namespace Project.api
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
              .Enrich.FromLogContext()
              .CreateLogger();

            try
            {
                Log.Information("Staring the Host");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host Terminated Unexpectedly");
            }

            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
          .UseSerilog((ctx, cfg) =>
          {
              var application = Environment.GetEnvironmentVariable("APPLICATION_NAME");
              var lokiUrl = Environment.GetEnvironmentVariable("LOKI_URL");

              if (string.IsNullOrEmpty(application))
                  application = ctx.HostingEnvironment.ApplicationName;

              if (string.IsNullOrEmpty(lokiUrl))
                  lokiUrl = "http://localhost:3100";

              var lokiLabels = new List<LokiLabel> { new LokiLabel { Key = "job", Value = application } };

              // Para adicionar o correlationId nos logs precisa habilitar na classe ApiConfig
              // o services.AddHttpContextAccessor();
              cfg.Enrich.WithProperty("Application", application);
              cfg.Enrich.WithCorrelationId();
              cfg.Enrich.WithCorrelationIdHeader("X-Correlation-Id");
              cfg.WriteTo.Console(theme: AnsiConsoleTheme.Code);
              cfg.WriteTo.GrafanaLoki(lokiUrl, lokiLabels);
          })
          .ConfigureWebHostDefaults(webBuilder =>
          {
              webBuilder.UseStartup<Startup>();
          });
    }
}
