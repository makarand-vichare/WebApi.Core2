using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Core.EntityModels.Location;

namespace WebApi.Core.Repositories.Configuration
{
    internal class CityConfiguration : IEntityTypeConfiguration<City>
    {

        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities").HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("bigint")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.HasOne(x => x.Country)
                .WithMany()
                .HasForeignKey(f => f.CountryId);

            //builder.Property(x => x.CountryId)
            //    .HasColumnName("CountryId")
            //    .HasColumnType("bigint")
            //    .IsRequired();

            builder.Property(x => x.CityName)
                .HasColumnName("CityName")
                .HasColumnType("nvarchar(200)")
                .IsRequired();

            builder.Property(x => x.IsActive)
            .HasColumnName("IsActive")
            .HasColumnType("bit")
            .IsRequired();
        }
    }
}
