using System;
using System.Collections.Generic;

namespace AlbumPrinter.Application.Querries.GetOrderDetails
{
    public class GetOrderDetailsResponse
    {
        public Guid OrderId { get; set; }
        public decimal RequiredSpace { get; set; }
        public Dictionary<string, int> OrderDetails { get; set; }
    }
}
