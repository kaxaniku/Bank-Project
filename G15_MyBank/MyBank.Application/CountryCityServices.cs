using MyBank.Application.Interfaces.Repositories;
using MyBank.Application.Interfaces.Services;
using MyBank.Domain;

namespace MyBank.Application;

public sealed class CountryCityServices : ICountryCityServices
{
    private readonly IUnitOfWork _unitOfWork;

    public CountryCityServices(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public IEnumerable<Country> ListAllCountries()
    {
        throw new NotImplementedException();
    }

    public Country? GetCountry(int countryId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<City> ListAllCities()
    {
        throw new NotImplementedException();
    }

    public City? GetCity(int cityId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<City> ListCitiesByCountry(int countryId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Country>> ListAllCountriesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Country?> GetCountryAsync(int countryId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<City>> ListAllCitiesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<City?> GetCityAsync(int cityId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<City>> ListCitiesByCountryAsync(int countryId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
