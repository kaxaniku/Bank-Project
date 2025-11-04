namespace MyBank.Infrastructure;

public class CityRepository : BaseRepository<Domain.City>, Interfaces.ICityRepository
{
    public CityRepository(BankDbContext context) : base(context) { }
}
