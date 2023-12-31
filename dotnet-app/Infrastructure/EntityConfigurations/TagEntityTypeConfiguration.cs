﻿using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class TagEntityTypeConfiguration : IEntityTypeConfiguration<TagDTO>
{
    public void Configure(EntityTypeBuilder<TagDTO> builder)
    {
        builder
            .ToTable("tags");

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.ToString(), 
                value => Guid.Parse(value));
        builder.HasKey(x => x.Id);

        builder
            .HasIndex(c => c.Name)
            .IsUnique();

        builder
            .HasMany(x => x.MessageTags)
            .WithOne(x => x.Tag);
    }
}
