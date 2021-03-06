using Microsoft.EntityFrameworkCore;
using RIL.Models;

namespace DAL.Data
{
    public class ProductModelSettings
    {
        private readonly ModelBuilder _builder;

        public ProductModelSettings(ModelBuilder builder)
        {
            _builder = builder;
        }

        public void AddSettings()
        {
            _builder.Entity<Product>()
               .HasKey(p => p.Id);

            _builder.Entity<Product>()
                .HasIndex(p => p.Name);
            _builder.Entity<Product>()
                .HasIndex(p => p.Platform);
            _builder.Entity<Product>()
                .HasIndex(p => p.DateCreated);
            _builder.Entity<Product>()
                .HasIndex(p => p.TotalRating);
            _builder.Entity<Product>()
                .HasIndex(p => p.Rating);
            _builder.Entity<Product>()
                .HasIndex(p => p.Price);
            _builder.Entity<Product>()
                .HasIndex(p => p.Genre);

            _builder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);
            _builder.Entity<Product>()
                .Property(p => p.Platform)
                .IsRequired();
            _builder.Entity<Product>()
                .Property(p => p.DateCreated)
                .IsRequired();
            _builder.Entity<Product>()
                .Property(p => p.TotalRating)
                .IsRequired();
            _builder.Entity<Product>()
                .Property(p => p.Genre)
                .IsRequired();
            _builder.Entity<Product>()
                .Property(p => p.Rating)
                .IsRequired();
            _builder.Entity<Product>()
               .Property(p => p.Logo)
               .IsRequired()
               .HasDefaultValue("https://firebasestorage.googleapis.com/v0/b/lab-web-app-299ef.appspot.com/o/empty_image.png?alt=media&token=fdbc866c-69d0-458e-a162-29a17eca00fe");
            _builder.Entity<Product>()
              .Property(p => p.Background)
              .IsRequired()
              .HasDefaultValue("https://firebasestorage.googleapis.com/v0/b/lab-web-app-299ef.appspot.com/o/empty_background.jpg?alt=media&token=0ad4ed61-2a4e-4b48-be4b-d683282d5fc5e");
            _builder.Entity<Product>()
                .Property(p => p.Price)
                .IsRequired();
            _builder.Entity<Product>()
                .Property(p => p.Count)
                .IsRequired();
            _builder.Entity<Product>()
                .Property(p => p.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            _builder.Entity<Product>()
                .HasMany(p => p.Users)
                .WithMany(p => p.Products)
                .UsingEntity<ProductRating>(
                    j => j
                        .HasOne(pr => pr.User)
                        .WithMany(u => u.Ratings)
                        .HasForeignKey(pr => pr.UserId),
                    j => j
                        .HasOne(pr => pr.Product)
                        .WithMany(p => p.Ratings)
                        .HasForeignKey(pr => pr.ProductId),
                    j =>
                    {
                        j.Property(pr => pr.Rating).HasDefaultValue(10.0);
                        j.HasKey(pr => new { pr.ProductId, pr.UserId });
                    });
            _builder.Entity<Product>()
                .HasMany(p => p.Orders)
                .WithMany(p => p.Products)
                .UsingEntity<ProductOrder>(
                    j => j
                        .HasOne(pr => pr.Order)
                        .WithMany(u => u.ProductOrders)
                        .HasForeignKey(pr => pr.OrderId),
                    j => j
                        .HasOne(pr => pr.Product)
                        .WithMany(p => p.ProductOrders)
                        .HasForeignKey(pr => pr.ProductId),
                    j =>
                    {
                        j.Property(pr => pr.ProductAmount).HasDefaultValue(1);
                        j.HasKey(pr => new { pr.ProductId, pr.OrderId });
                    });
        }
    }
}
