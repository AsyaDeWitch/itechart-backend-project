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
        }
    }
}
