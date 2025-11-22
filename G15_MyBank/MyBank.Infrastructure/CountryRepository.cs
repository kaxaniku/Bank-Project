namespace MyBank.Infrastructure;

internal class CountryRepository : BaseRepository<Domain.Country>, Application.Interfaces.ICountryRepository
{
    public CountryRepository(BankDbContext context) : base(context) { }
}
