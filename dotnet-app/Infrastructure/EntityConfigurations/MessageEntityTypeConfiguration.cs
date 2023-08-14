using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class MessageEntityTypeConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("messages");

        builder.Property<int>("Id")
            .HasColumnType("int")
            .UseIdentityColumn();
        builder.HasKey("Id");

        builder
            .HasMany(x => x.Tags)
            .WithMany("Messages");
    }
}
