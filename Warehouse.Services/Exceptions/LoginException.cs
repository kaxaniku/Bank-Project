namespace Warehouse.Services.Exceptions;

public class LoginException : Exception
{
    private const string MessageTemplate = "Login failed for user: '{0}'.";
    
    public LoginException(string username) : base(string.Format(MessageTemplate, username))
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
    }

    public string Username { get; }
}