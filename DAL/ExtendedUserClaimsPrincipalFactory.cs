using RIL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ExtendedUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ExtendedUser, IdentityRole<int>>
    {
        public ExtendedUserClaimsPrincipalFactory(UserManager<ExtendedUser> userManager, RoleManager<IdentityRole<int>> roleManager, IOptions<IdentityOptions> optionsAccessor)
            :base(userManager, roleManager, optionsAccessor)
        { }
        public async override Task<ClaimsPrincipal> CreateAsync(ExtendedUser user)
        {
            var principal = await base.CreateAsync(user);

            if (!string.IsNullOrWhiteSpace(user.AddressDelivery.ToString()))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] 
                {
                    new Claim("AddressDelivery", user.AddressDelivery.ToString())
                });
            }
            return principal;
        }
    }
}
