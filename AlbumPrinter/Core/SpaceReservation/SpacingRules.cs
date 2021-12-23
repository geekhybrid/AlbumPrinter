using AlbumPrinter.Core.SpaceReservation;
using System.Collections.Generic;

namespace AlbumPrinter.Core
{
    public static class SpacingRules
    {
        static SpacingRules()
        {
            OrderingRules.Add(OrderItemType.Mug, new MugStackingRule());
            OrderingRules.Add(OrderItemType.Calendar, new StandardItemRule(Sizes.CALENDAR_SIZE));
            OrderingRules.Add(OrderItemType.Canvas, new StandardItemRule(Sizes.CANVAS_SIZE));
            OrderingRules.Add(OrderItemType.PhotoBook, new StandardItemRule(Sizes.PHOTOBOOK_SIZE));
            OrderingRules.Add(OrderItemType.Card, new StandardItemRule(Sizes.CARD_SIZE));
        }

        public static decimal ComputeRequiredSpace(OrderItemType type, int count)
        {
            return OrderingRules[type].ComputeRequiredSpace(count);
        }

        private static Dictionary<OrderItemType, IOrderItemTypeSpacing> OrderingRules = new Dictionary<OrderItemType, IOrderItemTypeSpacing>();
    }
}
