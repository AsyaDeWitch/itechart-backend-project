using System.ComponentModel.DataAnnotations;

namespace BLL.ViewModels
{
    public class AssignRoleToUserViewModel
    {
        /// <summary>
        /// User Email
        /// </summary>
        /// <example>test@gmail.com</example>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Role name for assigning
        /// </summary>
        /// <example>User</example>
        [Required]
        public string RoleName{ get; set; }
    }
}
