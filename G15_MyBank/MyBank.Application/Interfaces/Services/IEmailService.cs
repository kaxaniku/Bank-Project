namespace MyBank.Application.Interfaces.Services
{
    internal interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
        Task SendEmailAsync(string to, string subject, string body, CancellationToken token);
    }
}
