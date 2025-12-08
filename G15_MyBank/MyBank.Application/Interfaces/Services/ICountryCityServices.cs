using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services;

public interface ICountryCityServices
{
    City GetCity(int cityId);
    Task<City> GetCityAsync(int cityId, CancellationToken cancellationToken);
    Country GetCountry(int countryId);
    Task<Country> GetCountryAsync(int countryId, CancellationToken cancellationToken);
    IEnumerable<City> ListAllCities(int pageNumber = 1);
    Task<IEnumerable<City>> ListAllCitiesAsync(CancellationToken cancellationToken, int pageNumber = 1);
    IEnumerable<Country> ListAllCountries();
    Task<IEnumerable<Country>> ListAllCountriesAsync(CancellationToken cancellationToken, int pageNumber = 1);
    IEnumerable<City> ListCitiesByCountry(int countryId, int pageNumber = 1);
    Task<IEnumerable<City>> ListCitiesByCountryAsync(int countryId, CancellationToken cancellationToken, int pageNumber = 1);
}