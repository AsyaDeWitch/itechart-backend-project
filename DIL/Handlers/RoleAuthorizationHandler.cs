using DIL.Requirements;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace DIL.Handlers
{
    public class RoleAuthorizationHandler : AuthorizationHandler<RoleAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleAuthorizationRequirement requirement)
        {
            var user = context.User;

            if(user != null)
            {
                if (user.IsInRole(requirement.Role))
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}
