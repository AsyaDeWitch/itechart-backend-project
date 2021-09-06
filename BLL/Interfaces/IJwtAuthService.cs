using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IJwtAuthService
    {
        public Task<IdentityUser<int>> SignInUserAsync(string email, string password);
        public Task<IdentityUser<int>> SignUpUserAsync(string email, string password);
        public Task<string> GenerateComfirmationLinkAsync(IdentityUser<int> user);
        public Task SendConfirmationLinkAsync(string userId, string confirmationLink);
        public Task<IdentityResult> ConfirmEmailAsync(string userId, string token);
        public string GenerateJwt(IdentityUser<int> user);
        
    }
}
