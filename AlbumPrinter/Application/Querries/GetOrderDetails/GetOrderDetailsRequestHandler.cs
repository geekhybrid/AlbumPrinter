using AlbumPrinter.Core;
using AlbumPrinter.Infrastructure.RequestHandling;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AlbumPrinter.Application.Querries.GetOrderDetails
{
    public class GetOrderDetailsRequestHandler : IRequestHandler<Guid, GetOrderDetailsResponse>
    {
        public GetOrderDetailsRequestHandler(IOrderRepository orderRepository) => _orderRepository = orderRepository;

        public async Task<GetOrderDetailsResponse> HandleAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var response = await _orderRepository.GetByIdAsync(id);

            if (response == null) return null;

            return new GetOrderDetailsResponse
            {
                OrderId = response.OrderId,
                RequiredSpace = response.RequiredSpace,
                OrderDetails = response
                    .OrderItemDescriptions?
                    .ToDictionary(item => item.ItemType.ToString(), item => item.Quantity)
            };
        }

        private IOrderRepository _orderRepository;
    }
}
