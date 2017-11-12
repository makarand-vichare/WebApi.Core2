using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Core.EntityModels.Queues;

namespace WebApi.Core.Repositories.Configuration
{
    internal class PdfQueueConfiguration : IEntityTypeConfiguration<PdfQueue>
    {

        public void Configure(EntityTypeBuilder<PdfQueue> builder)
        {
            builder.ToTable("PdfQueues").HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("bigint")
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(x => x.CriminalId)
                .HasColumnName("CriminalId")
                .HasColumnType("bigint")
                .IsRequired();

            builder.Property(x => x.GeneratedHtml)
                .HasColumnName("GeneratedHtml")
                .HasColumnType("nvarchar(MAX)");

            builder.Property(x => x.ReGenerationRequired)
                .HasColumnName("ReGenerationRequired")
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(x => x.IsPdfGenerationSucceed)
                .HasColumnName("IsPdfGenerationSucceed")
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(x => x.ErrorMessage)
                .HasColumnName("ErrorMessage")
                .HasColumnType("nvarchar(MAX)");
        }
    }
}
