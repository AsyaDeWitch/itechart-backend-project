using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RIL.Models;

namespace DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ExtendedUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductRating> ProductRatings { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var addressModelSettings = new AddressModelSettings(builder);
            addressModelSettings.AddSettings();

            var productModelSettings = new ProductModelSettings(builder);
            productModelSettings.AddSettings();

            var productRatingModelSettings = new ProductRatingModelSettings(builder);
            productRatingModelSettings.AddSettings();

            var orderModelSettings = new OrderModelSettings(builder);
            orderModelSettings.AddSettings();

            var productOrderModelSettings = new ProductOrderModelSettings(builder);
            productOrderModelSettings.AddSettings();

            var testDataProvider = new TestDataProvider(builder);
            testDataProvider.AddTestData();
        }
    }
}
