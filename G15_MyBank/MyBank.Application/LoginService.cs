using MyBank.Application.Interfaces.Repositories;
using MyBank.Application.Interfaces.Services;
using MyBank.Domain;

namespace MyBank.Application;

public sealed class LoginService : ILoginService
{
    private readonly IUnitOfWork _unitOfWork;

    public LoginService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Login?> GetLoginByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        var logins = await _unitOfWork.LoginRepository.QueryAsync(x => x.Username == username, cancellationToken);
        Login login = logins.FirstOrDefault()
            ?? throw new InvalidOperationException($"Login with user {username} does not exist.");
        if (!login.Activity.IsActive)
            throw new InvalidOperationException($"Login with user {username} no longer exists.");
        return login;
    }

    public async Task AddUserAsync(string username, string password, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(username);
        ArgumentException.ThrowIfNullOrWhiteSpace(password);
        var login = new Login
        {
            Username = username,
            PasswordHash = HashPassword(password),
            Activity = new ActivityInfo()
        };
        await _unitOfWork.LoginRepository.InsertAsync(login, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateUserPasswordAsync(string username, string newPassword, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(username);
        ArgumentException.ThrowIfNullOrWhiteSpace(newPassword);
        var login = await GetLoginByUsernameAsync(username, cancellationToken);
        login!.PasswordHash = HashPassword(newPassword);
        _unitOfWork.LoginRepository.Update(login);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveUserAsync(string username, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(username);
        var login = await GetLoginByUsernameAsync(username, cancellationToken);
        _unitOfWork.LoginRepository.Delete(login!);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ValidateUserAsync(string username, string password, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(username);
        ArgumentException.ThrowIfNullOrWhiteSpace(password);
        var login = await GetLoginByUsernameAsync(username, cancellationToken);
        if (login == null)
            return false;
        var hashedPassword = HashPassword(password);
        return login.PasswordHash == hashedPassword;
    }

    private string HashPassword(string password)
    {
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
    }
}
