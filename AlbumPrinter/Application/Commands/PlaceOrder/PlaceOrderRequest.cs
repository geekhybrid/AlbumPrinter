using AlbumPrinter.Core;
using System;
using System.Collections.Generic;

namespace AlbumPrinter.Commands.PlaceOrder
{
    public class PlaceOrderRequest
    {
        public Guid OrderId { get; set; }
        public Dictionary<string, int> OrderDetails { get; set; }
    }
}
