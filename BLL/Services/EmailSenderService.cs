using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using BLL.Interfaces;
using MimeKit;

namespace BLL.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly string _fromEmail = "itechartlabtester@gmail.com";
        private readonly string _fromPassword = "AV9Laaqpq9N5PZH1HemL";
        private readonly string _smtpClient = "smtp.gmail.com";
        private readonly int _port = 465;
        private readonly string _subject = "Email confirmation";

        public async Task SendEmailByNetMailAsync(string email, string htmlMessage)
        {
            MailMessage message = new();
            message.From = new MailAddress(_fromEmail);
            message.Subject = _subject;
            message.To.Add(new MailAddress(email));
            message.Body = GetHtmlBody(htmlMessage);
            message.IsBodyHtml = true;

            using (var smtpClient = new SmtpClient(_smtpClient)
            {
                Port = _port,
                Credentials = new NetworkCredential(_fromEmail, _fromPassword),
                EnableSsl = true,
            })
            {
                await smtpClient.SendMailAsync(message);
            };
        }

        public async Task SendEmailByMailKitAsync(string email, string htmlMessage)
        {
            MimeMessage message = new();
            message.From.Add(new MailboxAddress("LabWebApp tester",_fromEmail));
            message.To.Add(new MailboxAddress(email, email));
            message.Subject = _subject;
            message.Body = new BodyBuilder()
            {
                HtmlBody = GetHtmlBody(htmlMessage)
            }
            .ToMessageBody();

            using (var smtpClient = new MailKit.Net.Smtp.SmtpClient())
            {
                await smtpClient.ConnectAsync(_smtpClient, _port, true);
                await smtpClient.AuthenticateAsync(_fromEmail, _fromPassword);
                await smtpClient.SendAsync(message);

                await smtpClient.DisconnectAsync(true);
            }
        }

        private string GetHtmlBody(string message)
        {
            return "<html><body> " + message + " </body></html>";
        }
    }
}
