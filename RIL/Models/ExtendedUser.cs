using Microsoft.AspNetCore.Identity;

namespace RIL.Models

{
    public class ExtendedUser : IdentityUser<int>
    {
        public int? AddressDeliveryId { get; set; }
        public Address AddressDelivery { get; set; }
    }
}
