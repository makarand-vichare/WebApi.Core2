﻿using Microsoft.EntityFrameworkCore;
using WebApi.Core.EntityModels.Localization;

namespace WebApi.Core.Repositories.Configuration
{
    internal class KeyGroupConfiguration : IEntityTypeConfiguration<KeyGroup>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<KeyGroup> builder)
        {
            builder.ToTable("KeyGroups").HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("bigint")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.KeyGroupCode)
                .HasColumnName("KeyGroup")
                .HasColumnType("nvarchar(256)")
                .IsRequired();

            builder.Property(x => x.LocalizationKeys)
                .HasColumnName("LocalizationKeys")
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
        }
    }
}
