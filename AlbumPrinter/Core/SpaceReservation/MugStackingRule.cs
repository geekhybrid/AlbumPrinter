using AlbumPrinter.Core.SpaceReservation;
using System;

namespace AlbumPrinter.Core
{
    internal class MugStackingRule : IOrderItemTypeSpacing
    {
        public decimal ComputeRequiredSpace(int itemCount)
        {
            return Convert.ToDecimal(((itemCount / 4) * Sizes.MUG_SIZE) + ((itemCount % 4) * Sizes.MUG_SIZE));
        }
    }
}
