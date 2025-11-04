namespace MyBank.Infrastructure;

public class CountryRepository : BaseRepository<Domain.Country>, Interfaces.ICountryRepository
{
    public CountryRepository(BankDbContext context) : base(context) { }
}
