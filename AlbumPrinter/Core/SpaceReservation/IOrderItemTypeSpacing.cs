namespace AlbumPrinter.Core
{
    public interface IOrderItemTypeSpacing
    {
        decimal ComputeRequiredSpace(int itemCount);
    }
}
