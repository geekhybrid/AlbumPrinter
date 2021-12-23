using AlbumPrinter.Application.Querries.GetOrderDetails;
using AlbumPrinter.Commands.PlaceOrder;
using AlbumPrinter.Infrastructure.RequestHandling;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AlbumPrinter.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController
    {
        public OrdersController(
            IRequestHandler<PlaceOrderRequest, decimal> placeOrderHandler,
            IRequestHandler<Guid, GetOrderDetailsResponse> getOrderHandler
        )
        {
            _placeOrderHandler = placeOrderHandler;
            _getOrderHandler = getOrderHandler;
        }

        [HttpPost]
        public Task<decimal> PlaceOrder([FromBody] PlaceOrderRequest request, CancellationToken cancellationToken) => _placeOrderHandler.HandleAsync(request, cancellationToken);

        [HttpGet("id")]
        public Task<GetOrderDetailsResponse> GetById([FromQuery] Guid id, CancellationToken cancellationToken) => _getOrderHandler.HandleAsync(id, cancellationToken);

        private IRequestHandler<PlaceOrderRequest, decimal> _placeOrderHandler;
        private IRequestHandler<Guid, GetOrderDetailsResponse> _getOrderHandler;
    }
}
