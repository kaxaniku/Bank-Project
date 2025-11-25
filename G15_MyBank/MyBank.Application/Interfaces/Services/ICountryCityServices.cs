using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services;

public interface ICountryCityServices
{
    City? GetCity(int cityId);
    Task<City?> GetCityAsync(int cityId, CancellationToken cancellationToken);
    Country? GetCountry(int countryId);
    Task<Country?> GetCountryAsync(int countryId, CancellationToken cancellationToken);
    IEnumerable<City> ListAllCities();
    Task<IEnumerable<City>> ListAllCitiesAsync(CancellationToken cancellationToken);
    IEnumerable<Country> ListAllCountries();
    Task<IEnumerable<Country>> ListAllCountriesAsync(CancellationToken cancellationToken);
    IEnumerable<City> ListCitiesByCountry(int countryId);
    Task<IEnumerable<City>> ListCitiesByCountryAsync(int countryId, CancellationToken cancellationToken);
}