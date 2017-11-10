using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Core.EntityModels.Identity;

namespace WebApi.Core.Repositories.Configuration
{
    internal class ClaimConfiguration : IEntityTypeConfiguration<Claim>
    {

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Claim> builder)
        {
            builder.ToTable("Claims").HasKey(x => x.ClaimId);

            builder.Property(x => x.ClaimId)
                .HasColumnName("ClaimId")
                .HasColumnType("int")
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(x => x.UserId)
                .HasColumnName("UserId")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(x => x.ClaimType)
                .HasColumnName("ClaimType")
                .HasColumnType("nvarchar")
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(x => x.ClaimValue)
                .HasColumnName("ClaimValue")
                .HasColumnType("nvarchar")
                .HasMaxLength(500)
                .IsRequired(false);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Claims)
                .HasForeignKey(x => x.UserId);
        }
    }
}
