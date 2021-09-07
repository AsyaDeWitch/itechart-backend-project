using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class AssignRoleToUserViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string RoleName{ get; set; }
    }
}
