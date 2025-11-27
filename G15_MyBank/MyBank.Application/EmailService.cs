using System.Net.Mail;
using MyBank.Application.Interfaces;

namespace MyBank.Application;

public sealed class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(EmailSettings emailSettings)
    {
        _emailSettings = emailSettings;
    }

    public void SendEmail(string from, string to, string subject, string body)
    {
        using SmtpClient client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort);
        using MailMessage mailMessage = new MailMessage(from, to, subject, body);
        client.Send(mailMessage);
    }
}

public sealed class EmailSettings
{
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
}