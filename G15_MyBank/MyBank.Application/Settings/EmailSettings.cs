namespace MyBank.Application.Settings
{
    internal class EmailSettings
    {
        public string? SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string? Password { get; set; } = string.Empty;
        public string? FromAddress { get; set; } = string.Empty;
    }
}
