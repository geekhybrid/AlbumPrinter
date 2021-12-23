using System;
using System.Collections.Generic;

namespace AlbumPrinter.Core
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public virtual IList<OrderItemDescription> OrderItemDescriptions { get; set; }
        public decimal RequiredSpace { get; private set; }
        public void ComputeRequiredSpace()
        {
            foreach (var item in OrderItemDescriptions)
            {
                RequiredSpace += SpacingRules.ComputeRequiredSpace(item.ItemType, item.Quantity);
            }
        }
    }
}
