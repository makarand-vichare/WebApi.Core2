using Microsoft.EntityFrameworkCore;
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
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(x => x.KeyGroupCode)
                .HasColumnName("KeyGroup")
                .HasColumnType("nvarchar")
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.LocalizationKeys)
                .HasColumnName("LocalizationKeys")
                .HasColumnType("nvarchar")
                .HasMaxLength(5000)
                .IsRequired();
        }
    }
}
