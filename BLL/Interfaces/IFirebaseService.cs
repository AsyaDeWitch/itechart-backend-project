using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IFirebaseService
    {
        public Task<string> UploadLogoImageAsync(IFormFile logoImageFile);
        public Task<string> UploadBackgroundImageAsync(IFormFile backgroundImageFile);
    }
}
