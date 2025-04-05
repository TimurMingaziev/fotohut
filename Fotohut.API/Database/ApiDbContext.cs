using Fotohut.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Fotohut.API.Database;

public class ApiDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Contacts> Contacts { get; set; }
}