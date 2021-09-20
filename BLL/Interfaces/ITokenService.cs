using RIL.Models;

namespace BLL.Interfaces
{
    public interface ITokenService
    {
        public string BuildToken(ExtendedUser user, string issuer, string audience, string key);
        public bool IsTokenValid(string issuer, string audience, string key, string token);
        public string ExtractUserIdFromToken(string token);
    }
}
