using API.Network;
using Infrastructure.EntityConfigurations;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{
    builder.Configuration.AddJsonFile("/etc/secrets/appSecrets.json", false, true);
}

IConfiguration config = builder.Configuration;
Log.Logger = CreateSerilogLogger(config);

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(config.GetConnectionString("Default"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string[] origins = config.GetSection("Cors:AllowedHosts").Get<string[]>() ?? Array.Empty<string>();
CorsPolicy policy = new();
policy.Methods.Add("*");
policy.Headers.Add("*");
policy.SupportsCredentials = true;
foreach (string origin in origins)
{
    policy.Origins.Add(origin);
}

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Default", policy);
});

builder.Services.AddSignalR();
builder.Services.AddCustomServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    using (var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>())
    {
        try
        {
            appContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Migration proccess failed");
        }
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("Default");

app.MapControllers();
app.MapHub<MessageHub>("/messageHub");

app.Run();

Serilog.ILogger CreateSerilogLogger(IConfiguration config)
{
    return new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .ReadFrom.Configuration(config)
        .CreateLogger();
}