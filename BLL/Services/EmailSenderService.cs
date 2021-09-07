using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using BLL.Interfaces;
using MimeKit;

namespace BLL.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private string _fromEmail = "itechartlabtester@gmail.com";
        private string _fromPassword = "AV9Laaqpq9N5PZH1HemL";
        private string _smtpClient = "smtp.gmail.com";
        private int _port = 465;
        private string _subject = "Email confirmation";

        public async Task SendEmailByNetMailAsync(string email, string htmlMessage)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(_fromEmail);
            message.Subject = _subject;
            message.To.Add(new MailAddress(email));
            message.Body = "<html><body> " + htmlMessage + " </body></html>";
            message.IsBodyHtml = true;

            using (var smtpClient = new System.Net.Mail.SmtpClient(_smtpClient)
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
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("LabWebApp tester",_fromEmail));
            message.To.Add(new MailboxAddress(email, email));
            message.Subject = _subject;
            message.Body = new BodyBuilder()
            {
                HtmlBody = "<html><body> " + htmlMessage + " </body></html>"
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
    }
}
