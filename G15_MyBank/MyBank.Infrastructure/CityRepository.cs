namespace MyBank.Infrastructure;

internal class CityRepository : BaseRepository<Domain.City>, Application.Interfaces.ICityRepository
{
    public CityRepository(BankDbContext context) : base(context) { }
}
