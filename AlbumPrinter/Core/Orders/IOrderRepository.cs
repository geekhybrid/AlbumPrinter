using System;
using System.Threading.Tasks;

namespace AlbumPrinter.Core
{
    public interface IOrderRepository
    {
        Task SaveAsync(Order order);
        Task<Order> GetByIdAsync(Guid orderId);
    }
}
