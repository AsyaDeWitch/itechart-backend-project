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
        }
    }
}
