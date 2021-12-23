namespace AlbumPrinter.Infrastructure.Validations
{
    public interface IValidator<Request>
    {
        (bool isValid, string error) Validate(Request request);
    }
}
