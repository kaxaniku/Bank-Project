using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services;

public interface ICountryCityServices
{
    City GetCity(int cityId);
    Task<City> GetCityAsync(int cityId, CancellationToken cancellationToken);
    Country GetCountry(int countryId);
    Task<Country> GetCountryAsync(int countryId, CancellationToken cancellationToken);
    IEnumerable<City> ListAllCities();
    IAsyncEnumerable<City> ListAllCitiesAsync(CancellationToken cancellationToken);
    IEnumerable<Country> ListAllCountries();
    IAsyncEnumerable<Country> ListAllCountriesAsync(CancellationToken cancellationToken);
    IEnumerable<City> ListCitiesByCountry(int countryId);
    IAsyncEnumerable<City> ListCitiesByCountryAsync(int countryId, CancellationToken cancellationToken);
}