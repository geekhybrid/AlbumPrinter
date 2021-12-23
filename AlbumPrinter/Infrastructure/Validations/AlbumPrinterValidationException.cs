using System;

namespace AlbumPrinter.Infrastructure.Validations
{
    public class AlbumPrinterValidationException : Exception
    {
        public AlbumPrinterValidationException(string message): base(message)
        {}
    }
}
