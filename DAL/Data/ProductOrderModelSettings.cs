using Microsoft.EntityFrameworkCore;
using RIL.Models;

namespace DAL.Data
{
    public class ProductOrderModelSettings
    {
        private readonly ModelBuilder _builder;

        public ProductOrderModelSettings(ModelBuilder builder)
        {
            _builder = builder;
        }

        public void AddSettings()
        {
            _builder.Entity<ProductOrder>()
               .Property(po => po.ProductAmount)
               .IsRequired();
        }
    }
}
