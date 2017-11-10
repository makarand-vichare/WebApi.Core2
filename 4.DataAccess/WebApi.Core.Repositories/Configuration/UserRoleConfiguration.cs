using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Core.EntityModels.Identity;

namespace WebApi.Core.Repositories.Configuration
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {

        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles").HasKey(bc => new { bc.RoleId, bc.UserId });

            builder.Property(x => x.RoleId)
                .HasColumnName("RoleId")
                .HasColumnType("bigint")
                .IsRequired();

            builder.Property(x => x.UserId)
                .HasColumnName("UserId")
                .HasColumnType("bigint")
                .IsRequired();

            builder.HasOne(bc => bc.Role)
                .WithMany(b => b.UserRoles)
                .HasForeignKey(bc => bc.RoleId);

            builder.HasOne(bc => bc.User)
                .WithMany(c => c.UserRoles)
                .HasForeignKey(bc => bc.UserId);
        }
    }
}
