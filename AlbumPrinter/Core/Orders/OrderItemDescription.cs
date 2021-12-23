using System;

namespace AlbumPrinter.Core
{
    public class OrderItemDescription
    {
        public virtual Order Order { get; set; }
        public Guid OrderId { get; set; }
        public OrderItemType ItemType { get; set; }
        public int Quantity { get; set; }
    }
}
