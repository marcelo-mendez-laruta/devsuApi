using System.Text.Json.Serialization;
using devsuApi.Modules.Accounts;
using devsuApi.Modules.Clients;
using devsuApi.Modules.Movements;
using devsuApi.Modules.Reports;
using Microsoft.AspNetCore.Http.Json;
using AccountContracts = devsuApi.Modules.Accounts.Contracts;
using AccountServices = devsuApi.Modules.Accounts.Services;
using ClientContracts = devsuApi.Modules.Clients.Contracts;
using ClientServices = devsuApi.Modules.Clients.Services;
using Serilog;
using MovementsContracts = devsuApi.Modules.Movements.Contracts;
using MovementsServices = devsuApi.Modules.Movements.Services;
using ReportContracts = devsuApi.Modules.Reports.Contracts;
using ReportServices = devsuApi.Modules.Reports.Services;
var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
var connectionString = configuration["ConnectionStrings:DefaultConnection"]!;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AccountContracts>(provider => new AccountServices(new devsuApi.Context.DevsudbContext(connectionString)));
builder.Services.AddScoped<ClientContracts>(provider => new ClientServices(new devsuApi.Context.DevsudbContext(connectionString)));
builder.Services.AddScoped<MovementsContracts>(provider => new MovementsServices(new devsuApi.Context.DevsudbContext(connectionString)));
builder.Services.AddScoped<ReportContracts>(provider => new ReportServices(new devsuApi.Context.DevsudbContext(connectionString),provider.GetRequiredService<ILoggerFactory>()));
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
#region Serilog
// remove default logging providers
builder.Logging.ClearProviders();
// Serilog configuration		
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
// Register Serilog
builder.Logging.AddSerilog(logger);
#endregion
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", () => "Hello Devsu!");
app.MapAccountsEndpoints();
app.MapClientEndpoints();
app.MapMovementsEndpoint();
app.MapReportsEndpoint();
app.Run();
