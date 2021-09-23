using Microsoft.EntityFrameworkCore;
using RIL.Models;

namespace DAL.Data
{
    public class ProductRatingModelSettings
    {
        private readonly ModelBuilder _builder;

        public ProductRatingModelSettings(ModelBuilder builder)
        {
            _builder = builder;
        }

        public void AddSettings()
        {
            _builder.Entity<ProductRating>()
                .Property(pr => pr.Rating)
                .IsRequired();
        }
    }
}
