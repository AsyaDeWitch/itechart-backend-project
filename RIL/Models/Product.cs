using System;

namespace RIL.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Platform { get; set; }
        public DateTime DateCreated { get; set; }
        public double TotalRating { get; set; }
        public string Genre { get; set; }
        public int Rating { get; set; }
        public string Logo { get; set; }
        public string Background { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public bool IsDeleted { get; set; }
    }
}
