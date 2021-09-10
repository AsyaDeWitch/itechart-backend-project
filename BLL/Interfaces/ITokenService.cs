using Microsoft.AspNetCore.Identity;

namespace BLL.Interfaces
{
    public interface ITokenService
    {
        public string BuildToken(IdentityUser<int> user, string issuer, string audience, string key);
        public bool IsTokenValid(string issuer, string audience, string key, string token);
    }
}
