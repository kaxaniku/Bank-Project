using MyBank.Application.Interfaces.Services;
using MyBank.Application.Settings;
using System.Net;
using System.Net.Mail;

namespace MyBank.Application
{
    internal class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public void SendEmail(string to, string subject, string body)
        {
            var message = new MailMessage(_emailSettings.FromAddress!, to)
            {
                IsBodyHtml = true
            };

            using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
            {
                Credentials = new NetworkCredential(_emailSettings.FromAddress, _emailSettings.Password),
                EnableSsl = true
            };

            client.Send(message);
        }

        public async Task SendEmailAsync(string to, string subject, string body, CancellationToken token)
        {
            var message = new MailMessage(_emailSettings.FromAddress!, to)
            {
                IsBodyHtml = true
            };

            using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
            {
                Credentials = new NetworkCredential(_emailSettings.FromAddress, _emailSettings.Password),
                EnableSsl = true
            };

            client.SendAsync(message, token);
        }
    }
}
