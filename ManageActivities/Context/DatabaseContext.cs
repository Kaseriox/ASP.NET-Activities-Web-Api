using ManageActivities.Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace ManageActivities.Context;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Activity> Activities => Set<Activity>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(e => e.Username).IsUnique();
    }
}