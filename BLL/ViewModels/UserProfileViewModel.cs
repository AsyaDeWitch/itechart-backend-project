using System.ComponentModel.DataAnnotations;

namespace BLL.ViewModels
{
    public class UserProfileViewModel
    {
        /// <summary>
        /// User name
        /// </summary>
        /// <example>Asya</example>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// User phone number
        /// </summary>
        /// <example>=375290000000</example>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// User addess delivery
        /// </summary>
        public AddressViewModel AddressDelivery { get; set; } 
    }
}
