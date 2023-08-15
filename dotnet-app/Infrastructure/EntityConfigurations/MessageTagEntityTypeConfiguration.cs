using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class MessageTagEntityTypeConfiguration : IEntityTypeConfiguration<MessageTagDTO>
{
    public void Configure(EntityTypeBuilder<MessageTagDTO> builder)
    {
        builder.ToTable("MessageTags");

        builder.HasKey(x => new { x.MessageId, x.TagId });

        builder
            .HasOne(x => x.Message)
            .WithMany(x => x.MessageTags);

        builder
            .HasOne(x => x.Tag)
            .WithMany(x => x.MessageTags);
    }
}
