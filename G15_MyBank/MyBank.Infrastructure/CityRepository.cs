namespace MyBank.Infrastructure;

internal class CityRepository : BaseRepository<Domain.City>, Interfaces.ICityRepository
{
    public CityRepository(BankDbContext context) : base(context) { }
}
