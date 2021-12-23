using AlbumPrinter.Commands.PlaceOrder;
using AlbumPrinter.Infrastructure.Validations;

namespace AlbumPrinter.Application.Commands.PlaceOrder
{
    public class PlaceOrderRequestValidator : IValidator<PlaceOrderRequest>
    {
        public (bool isValid, string error) Validate(PlaceOrderRequest request)
        {
            return request.OrderDetails != null ? (true, null) : (false, "Invalid order details");
        }
    }
}
