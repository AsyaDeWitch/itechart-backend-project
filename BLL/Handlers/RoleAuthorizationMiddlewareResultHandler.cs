using BLL.Requiremets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BLL.Handlers
{
    public class RoleAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (Show403ForForbiddenResult(authorizeResult))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }

            await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }

        private bool Show403ForForbiddenResult(PolicyAuthorizationResult authorizeResult)
        {
            return authorizeResult.Forbidden 
                && authorizeResult.AuthorizationFailure.FailedRequirements
                .OfType<RoleAuthorizationRequirement>()
                .Any();
        }
    }
}
