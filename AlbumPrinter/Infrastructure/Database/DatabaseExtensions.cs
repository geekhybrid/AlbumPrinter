using AlbumPrinter.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AlbumPrinter.Infrastructure.Database
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection serviceCollection, ILoggerFactory loggerFactory = null)
        {
            serviceCollection.AddTransient<IOrderRepository, OrderRepository>();
            serviceCollection.AddDbContext<AlbumDatabaseContext>((dbContextOptionsBuilder) =>
            {
                dbContextOptionsBuilder
                        .UseSqlite("Data Source=albumPrinter.db;")
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors()
                        .UseLoggerFactory(loggerFactory);
            });

            serviceCollection.AddScoped<IAlbumDatabaseContext>(c => c.GetService<AlbumDatabaseContext>());

            return serviceCollection;
        }
    }
}
