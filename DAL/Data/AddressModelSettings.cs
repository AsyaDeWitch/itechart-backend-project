using Microsoft.EntityFrameworkCore;
using RIL.Models;

namespace DAL.Data
{
    public class AddressModelSettings
    {
        private readonly ModelBuilder _builder;

        public AddressModelSettings (ModelBuilder builder)
        {
            _builder = builder;
        }

        public void AddSettings()
        {
            _builder.Entity<Address>()
               .HasKey(a => a.Id);

            _builder.Entity<Address>()
                .Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(200);
            _builder.Entity<Address>()
                .Property(a => a.City)
                .IsRequired()
                .HasMaxLength(200);
            _builder.Entity<Address>()
                .Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(200);
            _builder.Entity<Address>()
                .Property(a => a.HouseNumber)
                .IsRequired();
            _builder.Entity<Address>()
                .Property(a => a.HouseBuilding)
                .HasDefaultValue("-")
                .HasMaxLength(20);
            _builder.Entity<Address>()
                .Property(a => a.EntranceNumber)
                .HasDefaultValue(1);
            _builder.Entity<Address>()
                .Property(a => a.FlatNumber)
                .IsRequired();
        }
    }
}
