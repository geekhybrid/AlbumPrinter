using AlbumPrinter.Core;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AlbumPrinter.Infrastructure.Database
{
    public class AlbumDatabaseContext : DbContext, IAlbumDatabaseContext
    {
        public AlbumDatabaseContext() {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public AlbumDatabaseContext(DbContextOptions options) : base(options) {}
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItemDescription> OrderItemDescriptions { get; set; }
    }
}
