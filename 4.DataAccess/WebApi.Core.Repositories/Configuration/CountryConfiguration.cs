using Microsoft.EntityFrameworkCore;
using WebApi.Core.EntityModels.Location;

namespace WebApi.Core.Repositories.Configuration
{
    internal class CountryConfiguration : IEntityTypeConfiguration<Country>
    {

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries").HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("bigint")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.CountryCode)
                .HasColumnName("CountryCode")
                .HasColumnType("nvarchar(256)")
                .IsRequired();

            builder.Property(x => x.CountryName)
                .HasColumnName("CountryName")
                .HasColumnType("nvarchar(200)")
                .IsRequired();

            builder.Property(x => x.IsActive)
            .HasColumnName("IsActive")
            .HasColumnType("bit")
            .IsRequired();
        }
    }
}
