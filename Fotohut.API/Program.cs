using Fotohut.API.Database;
using Fotohut.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Fotohut.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Migration Service
builder.Services.AddScoped<MigrationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => new List<string>()
{
    "/contacts"
});

app.MapGet("/contacts", async (ApiDbContext context) => await context.Contacts.ToListAsync());

app.MapGet("/cashedContacts", async ([FromServices] ApiDbContext context, [FromServices] IMemoryCache memoryCache) =>
{
    var contacts = await context.Contacts.ToListAsync();

    if (!memoryCache.TryGetValue("contacts", out List<Contacts>? cacheValue))
    {
        cacheValue = contacts;

        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromSeconds(5));

        memoryCache.Set("contacts", cacheValue, cacheEntryOptions);
    }

    return cacheValue;
});

// Add migration endpoint
app.MapPost("/api/migrate", async (MigrationService migrationService) =>
{
    await migrationService.MigrateAsync();
    return Results.Ok("Migration completed successfully");
})
.WithName("MigrateDatabase");

app.Run();