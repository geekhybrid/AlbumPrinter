using AlbumPrinter.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlbumPrinter.Infrastructure.Database.EntityConfiguration
{
    public class OrderItemDescriptionConfiguration : IEntityTypeConfiguration<OrderItemDescription>
    {
        public void Configure(EntityTypeBuilder<OrderItemDescription> builder)
        {
            builder.HasKey(e => new { e.OrderId, e.ItemType });
        }
    }
}
