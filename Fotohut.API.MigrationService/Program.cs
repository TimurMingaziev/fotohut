using Fotohut.API.Database;
using Fotohut.API.MigrationService;

var builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddNpgsqlDbContext<ApiDbContext>(connectionName: "postgresdb");

var host = builder.Build();
host.Run();