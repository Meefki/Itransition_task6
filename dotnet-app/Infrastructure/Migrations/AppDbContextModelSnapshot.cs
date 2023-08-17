﻿// <auto-generated />
using System;
using Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Infrastructure.Models.MessageDTO", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("SentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("messages", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Models.MessageTagDTO", b =>
                {
                    b.Property<string>("MessageId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TagId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MessageId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("MessageTags", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Models.TagDTO", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("tags", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Models.MessageTagDTO", b =>
                {
                    b.HasOne("Infrastructure.Models.MessageDTO", "Message")
                        .WithMany("MessageTags")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Models.TagDTO", "Tag")
                        .WithMany("MessageTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Message");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Infrastructure.Models.MessageDTO", b =>
                {
                    b.Navigation("MessageTags");
                });

            modelBuilder.Entity("Infrastructure.Models.TagDTO", b =>
                {
                    b.Navigation("MessageTags");
                });
#pragma warning restore 612, 618
        }
    }
}
