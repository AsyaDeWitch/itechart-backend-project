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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Address>()
                .HasKey(a => a.Id);
            builder.Entity<Address>()
                .Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(200);
            builder.Entity<Address>()
                .Property(a => a.City)
                .IsRequired()
                .HasMaxLength(200);
            builder.Entity<Address>()
                .Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(200);
            builder.Entity<Address>()
                .Property(a => a.HouseNumber)
                .IsRequired();
            builder.Entity<Address>()
                .Property(a => a.HouseBuilding)
                .HasDefaultValue("-")
                .HasMaxLength(20);
            builder.Entity<Address>()
                .Property(a => a.EntranceNumber)
                .HasDefaultValue(1);
            builder.Entity<Address>()
                .Property(a => a.FlatNumber)
                .IsRequired();

            builder.Entity<Product>()
                .HasKey(p => p.Id);
            builder.Entity<Product>()
                .HasIndex(p => p.Name);
            builder.Entity<Product>()
                .HasIndex(p => p.Platform);
            builder.Entity<Product>()
                .HasIndex(p => p.DateCreated);
            builder.Entity<Product>()
                .HasIndex(p => p.TotalRating);
            builder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);
            builder.Entity<Product>()
                .Property(p => p.Platform)
                .IsRequired();
            builder.Entity<Product>()
                .Property(p => p.DateCreated)
                .IsRequired();
            builder.Entity<Product>()
                .Property(p => p.TotalRating)
                .IsRequired();
        }
    }
}
