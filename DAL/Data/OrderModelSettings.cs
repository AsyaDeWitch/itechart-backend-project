using Microsoft.EntityFrameworkCore;
using RIL.Models;

namespace DAL.Data
{
    public class OrderModelSettings
    {
        private readonly ModelBuilder _builder;

        public OrderModelSettings(ModelBuilder builder)
        {
            _builder = builder;
        }

        public void AddSettings()
        {
            _builder.Entity<Order>()
               .HasKey(o => o.Id);

            _builder.Entity<Order>()
                .HasIndex(o => o.CreationDate);
            _builder.Entity<Order>()
                .HasIndex(o => o.Status);

            _builder.Entity<Order>()
                .Property(o => o.CreationDate)
                .IsRequired();
            _builder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .IsRequired();
            _builder.Entity<Order>()
                .Property(o => o.Status)
                .IsRequired()
                .HasDefaultValue((int)OrderStatus.Awaiting_Payment);
            _builder.Entity<Order>()
                .Property(o => o.DeliveryType)
                .IsRequired();

            _builder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders);
        }
    }
}
