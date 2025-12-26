using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services
{
    public interface ILoginService
    {
        Task AddUserAsync(string username, string password, CancellationToken cancellationToken);
        Task<Login?> GetLoginByUsernameAsync(string username, CancellationToken cancellationToken);
        Task RemoveUserAsync(string username, CancellationToken cancellationToken);
        Task UpdateUserPasswordAsync(string username, string newPassword, CancellationToken cancellationToken);
        Task<bool> ValidateUserAsync(string username, string password, CancellationToken cancellationToken);
    }
}