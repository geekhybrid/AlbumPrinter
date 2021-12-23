using AlbumPrinter.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Refyne.Database.Configuration.EntityTypeConfiguration
{
    public class PatientConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.Id);

            builder
                .HasMany(e => e.OrderItemDescriptions)
                .WithOne(e => e.Order)
                .HasForeignKey(e => e.OrderId);
        }
    }
}