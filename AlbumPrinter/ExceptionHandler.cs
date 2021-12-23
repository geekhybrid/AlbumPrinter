using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace AlbumPrinter
{
    public class ExceptionHandler
    {
        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            return context.Response.WriteAsync(exception.Message);
        }

        private readonly RequestDelegate _next;
    }
}
