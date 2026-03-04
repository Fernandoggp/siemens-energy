using Microsoft.EntityFrameworkCore;
using Project.Api.Configurations;
using Project.Repository.Persistence;
using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

// ===========================
// DATABASE (EF Core)
// ===========================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"))
);

// ===========================
// API VERSIONING + EXPLORER
// ===========================
builder.Services
    .AddApiVersioning(options =>
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.ReportApiVersions = true;
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'V";
        options.SubstituteApiVersionInUrl = true;
    });

// ===========================
// DEPENDENCY INJECTION
// ===========================
builder.Services.AddDependencyInjectionConfiguration(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();