using AlbumPrinter.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumPrinter.Infrastructure.Database
{
    public class OrderRepository : IOrderRepository
    {
        public OrderRepository(IAlbumDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Order> GetByIdAsync(Guid id)
        {
            return _dbContext.Set<Order>()
                .AsQueryable()
                .Include(order => order.OrderItemDescriptions)
                .FirstOrDefaultAsync(order => order.OrderId == id);
        }

        public async Task SaveAsync(Order order)
        {
            await _dbContext.Set<Order>().AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }

        private IAlbumDatabaseContext _dbContext;
    }
}
