using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("tags");

        builder.Property<int>("id")
            .HasColumnType("int")
            .UseIdentityColumn();
        builder.HasKey("id");

        builder
            .HasIndex(c => c.Name)
            .IsUnique();
    }
}
