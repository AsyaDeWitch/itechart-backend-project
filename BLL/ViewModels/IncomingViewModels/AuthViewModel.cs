using System.ComponentModel.DataAnnotations;

namespace BLL.ViewModels
{
    public class AuthViewModel
    {
        /// <summary>
        /// User email
        /// </summary>
        /// <example>test@gmail.com</example>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        /// <example>Password1!</example>
        [Required]
        public string Password { get; set; }
    }
}
