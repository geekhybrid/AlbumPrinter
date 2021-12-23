using AlbumPrinter.Application.Commands.PlaceOrder;
using AlbumPrinter.Commands.PlaceOrder;
using AlbumPrinter.Core;
using AlbumPrinter.Core.SpaceReservation;
using AlbumPrinter.Infrastructure.Validations;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumPrinter.Tests.UnitTests.Commands
{
    public class WhenPlaceOrder
    {

        [Test]
        public void Should_Throw_Validation_Exception_If_Invalid()
        {
            var validator = new Mock<IValidator<PlaceOrderRequest>>();
            validator.Setup(v => v.Validate(It.IsAny<PlaceOrderRequest>())).Returns((false, "Invalid request"));

            var order = new PlaceOrderRequest();

            var orderHandler = new PlaceOrderHandler(null, validator.Object, new Mock<ILogger<PlaceOrderHandler>>().Object);

            Assert.ThrowsAsync<AlbumPrinterValidationException>(() => orderHandler.HandleAsync(order));
        }

        [Test]
        public async Task Should_Create_Order_And_Compute_RequiredSpace()
        {
            var orderRepository = new Mock<IOrderRepository>();
            var validator = new Mock<IValidator<PlaceOrderRequest>>();
            validator.Setup(v => v.Validate(It.IsAny<PlaceOrderRequest>())).Returns((true, null));

            var itemQuantity = 1;
            decimal standardCalendarSize = 10;
            var itemType = OrderItemType.Calendar;

            var order = new PlaceOrderRequest
            {
                OrderId = Guid.NewGuid(),
                OrderDetails = new Dictionary<string, int>()
                {
                    { itemType.ToString(), itemQuantity }
                }
            };

            var orderHandler = new PlaceOrderHandler(orderRepository.Object, validator.Object, logger);
            var response = await orderHandler.HandleAsync(order);

            orderRepository.Verify(
                orderRepository => orderRepository.SaveAsync(It.Is<Order>(
                    o => o.OrderId == order.OrderId
                    && o.OrderItemDescriptions.Single().ItemType == itemType
                    && o.OrderItemDescriptions.Single().Quantity == itemQuantity
                    && o.RequiredSpace == standardCalendarSize)), Times.Once);
        }

        [Test]
        [TestCase(4, Sizes.MUG_SIZE)]
        [TestCase(5, Sizes.MUG_SIZE + Sizes.MUG_SIZE)]
        public async Task Should_Apply_Custom_SpacingRule_For_Mugs(int numberOfMusgs, float expectedSpace)
        {
            var orderRepository = new Mock<IOrderRepository>();
            var validator = new Mock<IValidator<PlaceOrderRequest>>();
            validator.Setup(v => v.Validate(It.IsAny<PlaceOrderRequest>())).Returns((true, null));

            var order = new PlaceOrderRequest
            {
                OrderId = Guid.NewGuid(),
                OrderDetails = new Dictionary<string, int> { { OrderItemType.Mug.ToString(), numberOfMusgs } }
            };

            var orderHandler = new PlaceOrderHandler(orderRepository.Object, validator.Object, logger);
            var response = await orderHandler.HandleAsync(order);

            orderRepository.Verify(
                orderRepository => orderRepository.SaveAsync(It.Is<Order>(
                    o => o.OrderId == order.OrderId
                    && o.RequiredSpace == (decimal)expectedSpace)), Times.Once);
        }

        private ILogger<PlaceOrderHandler> logger = new Mock<ILogger<PlaceOrderHandler>>().Object;
    }
}
