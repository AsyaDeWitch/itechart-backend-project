using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IEmailSenderService
    {
        public Task SendEmailByNetMailAsync(string email, string htmlMessage);
        public Task SendEmailByMailKitAsync(string email, string htmlMessage);
    }
}
