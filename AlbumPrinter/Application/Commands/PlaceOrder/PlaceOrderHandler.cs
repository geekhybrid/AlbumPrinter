using AlbumPrinter.Commands.PlaceOrder;
using AlbumPrinter.Core;
using AlbumPrinter.Infrastructure.RequestHandling;
using AlbumPrinter.Infrastructure.Validations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AlbumPrinter.Application.Commands.PlaceOrder
{
    public class PlaceOrderHandler : IRequestHandler<PlaceOrderRequest, decimal>
    {
        public PlaceOrderHandler(
            IOrderRepository orderRepository, 
            IValidator<PlaceOrderRequest> validator,
            ILogger<PlaceOrderHandler> logger)
        {
            _orderRepository = orderRepository;
            _requestValidator = validator;
            _logger = logger;
        }

        public async Task<decimal> HandleAsync(PlaceOrderRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Validating order.");
            var validationResult = _requestValidator.Validate(request);

            if (!validationResult.isValid)
            {
                _logger.LogError($"Validation Error: Error occured while placing order with id {request.OrderId}");
                throw new AlbumPrinterValidationException("Invalid order request");
            }

            var order = new Order
            {
                OrderId = request.OrderId,
                OrderItemDescriptions = new List<OrderItemDescription>()
            };

            foreach (var item in request.OrderDetails)
            {
                order.OrderItemDescriptions.Add(
                    new OrderItemDescription {
                        ItemType = (OrderItemType)Enum.Parse(typeof(OrderItemType), item.Key),
                        Order = order,
                        Quantity = item.Value
                    }
                );
            }

            order.ComputeRequiredSpace();
            await _orderRepository.SaveAsync(order);
            _logger.LogInformation($"Order with ID {order.OrderId} has been saved.");

            return order.RequiredSpace;
        }

        private readonly IOrderRepository _orderRepository;
        private readonly IValidator<PlaceOrderRequest> _requestValidator;
        private readonly ILogger<PlaceOrderHandler> _logger;
    }
}
