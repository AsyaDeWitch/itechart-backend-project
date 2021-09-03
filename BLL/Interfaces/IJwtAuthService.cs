using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IJwtAuthService
    {
        public Task<IdentityUser<int>> SignInUserAsync(string email, string password);
        public Task<IdentityUser<int>> SignUpUserAsync(string email, string password);
        public string GenerateJwt(IdentityUser<int> user);
    }
}
