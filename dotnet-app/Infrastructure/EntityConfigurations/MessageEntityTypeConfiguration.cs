using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class MessageEntityTypeConfiguration : IEntityTypeConfiguration<MessageDTO>
{
    public void Configure(EntityTypeBuilder<MessageDTO> builder)
    {
        builder
            .ToTable("messages");

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.ToString(),
                value => Guid.Parse(value));
        builder.HasKey(x => x.Id);

        builder
            .HasMany(x => x.MessageTags)
            .WithOne(x => x.Message);
    }
}
