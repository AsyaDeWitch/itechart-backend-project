using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace BLL.Services
{
    public class EmailSenderService : IEmailSender
    {
        private string _fromEmail = "from@example.com";
        private string _fromName = "aa44ecc3e14def"; 
        private string _fromPassword = "be45025b5c91aa";

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(_fromEmail);
            message.Subject = subject;
            message.To.Add(new MailAddress(email));
            message.Body = "<html><body> " + htmlMessage + " </body></html>";
            message.IsBodyHtml = true;

            using (var smtpClient = new SmtpClient("smtp.mailtrap.io")
            {
                Port = 2525,
                Credentials = new NetworkCredential(_fromName, _fromPassword),
                EnableSsl = true,
            })
            {
                await smtpClient.SendMailAsync(message);
            };
        }
    }
}
