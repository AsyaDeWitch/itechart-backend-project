using Microsoft.AspNetCore.Authorization;

namespace BLL.Requiremets
{
    public class RoleAuthorizationRequirement : IAuthorizationRequirement
    {
        public string Role { get; private set; }

        public RoleAuthorizationRequirement(string role)
        {
            Role = role;
        }
    }
}
