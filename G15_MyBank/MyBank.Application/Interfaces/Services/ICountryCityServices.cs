using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services;

public interface ICountryCityServices
{
    City GetCity(int cityId);
    Task<City> GetCityAsync(int cityId, CancellationToken cancellationToken);
    Country GetCountry(int countryId);
    Task<Country> GetCountryAsync(int countryId, CancellationToken cancellationToken);
    IEnumerable<City> ListAllCities(int pageSize = 10, int pageNumber = 1);
    Task<IEnumerable<City>> ListAllCitiesAsync(CancellationToken cancellationToken, int pageSize = 10, int pageNumber = 1);
    IEnumerable<Country> ListAllCountries(int pageSize = 10, int pageNumber = 1);
    Task<IEnumerable<Country>> ListAllCountriesAsync(CancellationToken cancellationToken, int pageSize = 10, int pageNumber = 1);
    IEnumerable<City> ListCitiesByCountry(int countryId, int pageSize = 10, int pageNumber = 1);
    Task<IEnumerable<City>> ListCitiesByCountryAsync(int countryId, CancellationToken cancellationToken, int pageSize = 10, int pageNumber = 1);
}