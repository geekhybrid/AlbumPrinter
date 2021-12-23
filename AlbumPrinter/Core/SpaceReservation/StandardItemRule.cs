namespace AlbumPrinter.Core
{
    internal class StandardItemRule : IOrderItemTypeSpacing
    {
        internal StandardItemRule(decimal singleItemSpace)
        {
            _singleItemSpace = singleItemSpace;
        }

        public decimal ComputeRequiredSpace(int itemCount) => _singleItemSpace * itemCount;

        private decimal _singleItemSpace;
    }
}
