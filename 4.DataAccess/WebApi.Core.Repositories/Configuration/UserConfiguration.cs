using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Core.EntityModels.Identity;

namespace WebApi.Core.Repositories.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users").HasKey(x => x.Id);

            builder.Property(x => x.Id)
            .HasColumnName("UserId")
            .HasColumnType("bigint")
            .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnName("Email")
                .HasColumnType("nvarchar")
                .HasMaxLength(256);

            builder.Property(x => x.DateOfBirth)
                .HasColumnName("BirthDate")
                .HasColumnType("date");

            builder.Property(x => x.CityId)
               .HasColumnName("CityId")
               .HasColumnType("bigint");

            builder.Property(x => x.CountryId)
               .HasColumnName("CountryId")
               .HasColumnType("bigint");

            builder.Property(x => x.Gender)
              .HasColumnName("Gender")
              .HasColumnType("int");

            builder.Property(x => x.PasswordHash)
                .HasColumnName("PasswordHash")
                .HasColumnType("nvarchar");

            builder.Property(x => x.SecurityStamp)
                .HasColumnName("SecurityStamp")
                .HasColumnType("nvarchar");

            builder.Property(x => x.UserName)
                .HasColumnName("UserName")
                .HasColumnType("nvarchar")
                .HasMaxLength(256)
                .IsRequired();

            builder.HasMany(x => x.Claims)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.Logins)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}
