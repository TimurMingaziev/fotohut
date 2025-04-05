using Fotohut.API.Database;
using Fotohut.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddMemoryCache();

builder.AddNpgsqlDbContext<ApiDbContext>(connectionName: "postgresdb");

var app = builder.Build();

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

app.Run();