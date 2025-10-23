using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure.Persistence;

public class BankSystemDbContext(DbContextOptions<BankSystemDbContext> options) : IdentityDbContext(options)
{
}
