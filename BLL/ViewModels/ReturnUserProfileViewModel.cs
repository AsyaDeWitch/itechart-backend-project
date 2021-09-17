using System.ComponentModel.DataAnnotations;

namespace BLL.ViewModels
{
    public class ReturnUserProfileViewModel
    {
        /// <summary>
        /// User name
        /// </summary>
        /// <example>Asya</example>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        /// <example>test@gmail.com</example>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// User phone number
        /// </summary>
        /// <example>+375290000000</example>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// User Delivery address
        /// </summary>
        public AddressViewModel AddressDelivery { get; set; }
    }
}
