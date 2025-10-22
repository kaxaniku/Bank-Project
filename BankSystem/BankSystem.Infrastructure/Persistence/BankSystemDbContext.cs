using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure.Persistence;

public class BankSystemDbContext : IdentityDbContext
{
    public BankSystemDbContext(DbContextOptions<BankSystemDbContext> options)
        : base(options)
    {
    }
}
