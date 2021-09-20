using RIL.Models;

namespace BLL.ViewModels
{
    public class UserProfileViewModel
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public Address AddressDelivery { get; set; } 
    }
}
