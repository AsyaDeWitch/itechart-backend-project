using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAuthService
    {
        public Task<(IdentityUser<int>, string)> SignInUserAsync(string email, string password, string issuer, string audience, string key);
        public Task<IdentityUser<int>> SignUpUserAsync(string email, string password);
        public Task<string> GenerateComfirmationLinkAsync(IdentityUser<int> user);
        public Task SendConfirmationLinkAsync(string userId, string confirmationLink);
        public Task<IdentityResult> ConfirmEmailAsync(string userId, string token);
    }
}
