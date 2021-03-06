﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Core.EntityModels.Identity;

namespace WebApi.Core.Repositories.Configuration
{
    internal class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients").HasKey(x => x.ClientId);

                builder.Property(x => x.ClientId)
                .HasColumnName("ClientId")
                .HasColumnType("nvarchar(200)")
                .IsRequired();

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("bigint")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Secret)
                .HasColumnName("Secret")
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder.Property(x => x.ApplicationType)
                .HasColumnName("ApplicationType")
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(x => x.Active)
                .HasColumnName("Active")
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(x => x.RefreshTokenLifeTime)
                .HasColumnName("RefreshTokenLifeTime")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.AllowedOrigin)
                .HasColumnName("AllowedOrigin")
                .HasColumnType("nvarchar(200)")
                .IsRequired();

            builder.HasMany(x => x.RefreshTokens)
                .WithOne(x => x.Client)
                .HasForeignKey(x => x.ClientId);
        }
    }
}
