using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services;

public interface ICountryCityServices
{
    City? GetCity(int cityId);
    Task<City?> GetCityAsync(int cityId);
    Country? GetCountry(int countryId);
    Task<Country?> GetCountryAsync(int countryId);
    IEnumerable<City> ListAllCities();
    Task<IEnumerable<City>> ListAllCitiesAsync();
    IEnumerable<Country> ListAllCountries();
    Task<IEnumerable<Country>> ListAllCountriesAsync();
    IEnumerable<City> ListCitiesByCountry(int countryId);
    Task<IEnumerable<City>> ListCitiesByCountryAsync(int countryId);
}