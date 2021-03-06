﻿using Microsoft.EntityFrameworkCore;
using WebApi.Core.EntityModels.Identity;

namespace WebApi.Core.Repositories.Configuration
{
    internal class ClaimConfiguration : IEntityTypeConfiguration<Claim>
    {

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Claim> builder)
        {
            builder.ToTable("Claims").HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ClaimId")
                .HasColumnType("bigint")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.UserId)
                .HasColumnName("UserId")
                .HasColumnType("bigint")
                .IsRequired();

            builder.Property(x => x.ClaimType)
                .HasColumnName("ClaimType")
                .HasColumnType("nvarchar(500)")
                .IsRequired(false);

            builder.Property(x => x.ClaimValue)
                .HasColumnName("ClaimValue")
                .HasColumnType("nvarchar(500)")
                .IsRequired(false);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Claims)
                .HasForeignKey(x => x.UserId);
        }
    }
}
