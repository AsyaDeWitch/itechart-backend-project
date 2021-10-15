using System.ComponentModel.DataAnnotations;

namespace BLL.ViewModels
{
    public class UserProfileViewModel
    {
        /// <summary>
        /// User name
        /// </summary>
        /// <example>Test</example>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// User phone number
        /// </summary>
        /// <example>+375290000000</example>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// User address delivery
        /// </summary>
        public AddressViewModel AddressDelivery { get; set; } 
    }
}
