using RIL.Models;

namespace BLL.ViewModels
{
    public class ReturnUserProfileViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Address AddressDelivery { get; set; }
    }
}
