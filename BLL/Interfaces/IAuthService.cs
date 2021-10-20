using Microsoft.AspNetCore.Identity;
using RIL.Models;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAuthService
    {
        public Task<(ExtendedUser, string)> SignInUserAsync(string email, string password, string issuer, string audience, string key);
        public Task<ExtendedUser> SignUpUserAsync(string email, string password);
        public Task<string> GenerateConfirmationLinkAsync(ExtendedUser user);
        public Task SendConfirmationLinkAsync(string userId, string confirmationLink);
        public Task<IdentityResult> ConfirmEmailAsync(string userId, string token);
    }
}
