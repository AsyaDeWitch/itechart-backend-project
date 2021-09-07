using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class AuthModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
