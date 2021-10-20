using Microsoft.AspNetCore.Authorization;

namespace DIL.Requirements
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
