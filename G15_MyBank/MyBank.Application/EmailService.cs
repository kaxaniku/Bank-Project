using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using MyBank.Application.Interfaces.Services;

namespace MyBank.Application;

public sealed class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public void SendEmail(string to, string subject, string body)
    {
        var message = new MailMessage(_emailSettings.FromAddress, to, subject, body)
        {
            IsBodyHtml = true
        };

        using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort);
        client.Credentials = new NetworkCredential(_emailSettings.FromAddress, _emailSettings.Password);
        client.EnableSsl = true;

        client.Send(message);
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var message = new MailMessage(_emailSettings.FromAddress, to, subject, body)
        {
            IsBodyHtml = true
        };

        using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort);
        client.Credentials = new NetworkCredential(_emailSettings.FromAddress, _emailSettings.Password);
        client.EnableSsl = true;

        await client.SendMailAsync(message);
    }
}

public sealed class EmailSettings
{
    public string? SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string Password { get; set; } = string.Empty;
    public string FromAddress { get; set; } = string.Empty;
}