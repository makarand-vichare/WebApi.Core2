using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Core.EntityModels.Identity;

namespace WebApi.Core.Repositories.Configuration
{
    internal class ExternalLoginConfiguration : IEntityTypeConfiguration<ExternalLogin>
    {
        public void Configure(EntityTypeBuilder<ExternalLogin> builder)
        {
            builder.ToTable("ExternalLogins").HasKey(x => new { x.LoginProvider, x.ProviderKey, x.UserId });

            builder.Property(x => x.LoginProvider)
                .HasColumnName("LoginProvider")
                .HasColumnType("nvarchar(128)")
                .IsRequired();

            builder.Property(x => x.ProviderKey)
                .HasColumnName("ProviderKey")
                .HasColumnType("nvarchar(128)")
                .IsRequired();

            builder.Property(x => x.UserId)
                .HasColumnName("UserId")
                .HasColumnType("bigint")
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Logins)
                .HasForeignKey(x => x.UserId);
        }
    }
}
