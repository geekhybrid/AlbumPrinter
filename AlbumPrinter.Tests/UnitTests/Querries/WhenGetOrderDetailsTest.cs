using AlbumPrinter.Application.Querries.GetOrderDetails;
using AlbumPrinter.Core;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumPrinter.Tests.UnitTests.Querries
{
    public class WhenGetOrderDetailsTest
    {
        [Test]
        public async Task Should_Return_OrderDetails_When_GetById()
        {
            var orderRepository = new Mock<IOrderRepository>();
            var orderId = Guid.NewGuid();
            var expectedOrder = new Order()
            {
                OrderId = orderId,
                OrderItemDescriptions = new List<OrderItemDescription>
                {
                    new OrderItemDescription
                    {
                        ItemType = OrderItemType.Mug,
                        Quantity = 2
                    }
                }
            };
            expectedOrder.ComputeRequiredSpace();

            orderRepository.Setup(repo => repo.GetByIdAsync(orderId))
                .ReturnsAsync(expectedOrder);

            var getByIdHandler = new GetOrderDetailsRequestHandler(orderRepository.Object);
            var response = await getByIdHandler.HandleAsync(orderId);

            orderRepository.Verify(
                orderRepository => orderRepository.GetByIdAsync(orderId), Times.Once);

            Assert.AreEqual(response.RequiredSpace, expectedOrder.RequiredSpace);
            Assert.AreEqual(response.OrderId, orderId);
            Assert.AreEqual(response.OrderDetails.Single().Key, expectedOrder.OrderItemDescriptions.Single().ItemType.ToString());
            Assert.AreEqual(response.OrderDetails.Single().Value, expectedOrder.OrderItemDescriptions.Single().Quantity);

        }
    }
}
