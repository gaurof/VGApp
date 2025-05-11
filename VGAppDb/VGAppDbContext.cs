using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VGAppDb.Models;

namespace VGAppDb;

public class VGAppDbContext : DbContext
{
    public VGAppDbContext(DbContextOptions<VGAppDbContext> options)
        : base(options)
    {
        Database.Migrate();
    }
    public DbSet<User> Users { get; set; }
}
