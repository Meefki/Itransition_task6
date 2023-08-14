using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityConfigurations;

public class AppDbContext : DbContext
{
    public DbSet<Message> Messages { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TagEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new MessageEntityTypeConfiguration());
    }
}
