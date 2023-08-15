using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityConfigurations;

public class AppDbContext : DbContext
{
    public DbSet<MessageDTO> Messages { get; set; }
    public DbSet<MessageTagDTO> MessageTags { get; set; }
    public DbSet<TagDTO> Tags { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TagEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new MessageEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new MessageTagEntityTypeConfiguration());
    }
}
