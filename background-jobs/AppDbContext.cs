using background_jobs.Models;
using Microsoft.EntityFrameworkCore;

namespace background_jobs;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>().HasKey(x => x.Id);
        base.OnModelCreating(modelBuilder);
    }
}