using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Core.EntityModels.Identity;

namespace WebApi.Core.Repositories.Configuration
{
    internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens").HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("bigint")
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(x => x.TokenId)
                .HasColumnName("TokenId")
                .HasColumnType("nvarchar")
                .IsRequired();

            builder.Property(x => x.Subject)
                .HasColumnName("Subject")
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.ClientId)
                .HasColumnName("ClientId")
                .HasColumnType("nvarchar")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.IssuedUtc)
                .HasColumnName("IssuedUtc")
                .HasColumnType("DateTime")
                .IsRequired();

            builder.Property(x => x.ExpiresUtc)
                .HasColumnName("ExpiresUtc")
                .HasColumnType("DateTime")
                .IsRequired();

            builder.Property(x => x.ProtectedTicket)
                .HasColumnName("ProtectedTicket")
                .HasColumnType("nvarchar")
                .IsRequired();

            builder.HasOne(x => x.Client)
                .WithMany(x => x.RefreshTokens)
                .HasForeignKey(x => x.ClientId);
        }
    }
}
