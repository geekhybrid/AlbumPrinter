using AlbumPrinter.Application.Commands.PlaceOrder;
using AlbumPrinter.Application.Querries.GetOrderDetails;
using AlbumPrinter.Commands.PlaceOrder;
using AlbumPrinter.Infrastructure.APIDocs;
using AlbumPrinter.Infrastructure.Database;
using AlbumPrinter.Infrastructure.RequestHandling;
using AlbumPrinter.Infrastructure.Validations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AlbumPrinter
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDatabaseServices();
            services.AddControllers();

            services.AddTransient<IRequestHandler<PlaceOrderRequest, decimal>, PlaceOrderHandler>();
            services.AddTransient<IValidator<PlaceOrderRequest>, PlaceOrderRequestValidator>();
            services.AddTransient<IRequestHandler<Guid, GetOrderDetailsResponse>, GetOrderDetailsRequestHandler>();

            services.AddSwaggerDocumentation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionHandler>();
            app.UseRouting();
            app.UseSwaggerDocumentation();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
