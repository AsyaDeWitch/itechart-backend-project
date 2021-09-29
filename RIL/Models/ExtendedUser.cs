using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace RIL.Models

{
    public class ExtendedUser : IdentityUser<int>
    {
        public int? AddressDeliveryId { get; set; }
        public Address AddressDelivery { get; set; }
        public ICollection<Product> Products { get; set; }
        public List<ProductRating> Ratings { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
