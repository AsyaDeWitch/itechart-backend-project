using System;

namespace RIL.Models
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Platform { get; set; }
        public DateTime DateCreated { get; set; }
        public double TotalRating { get; set; }
    }
}
