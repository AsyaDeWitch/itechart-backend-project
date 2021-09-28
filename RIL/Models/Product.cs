using System;
using System.Collections.Generic;

namespace RIL.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Platform { get; set; }
        public DateTime DateCreated { get; set; }
        public double TotalRating { get; set; }
        public int Genre { get; set; }
        public int Rating { get; set; }
        public string Logo { get; set; }
        public string Background { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<ExtendedUser> Users { get; set; }
        public List<ProductRating> Ratings { get; set; }
        public ICollection<Order> Orders { get; set; }
        public List<ProductOrder> ProductOrders { get; set; }
    }
}
