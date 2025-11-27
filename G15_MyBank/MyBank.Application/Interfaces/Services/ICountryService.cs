using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services
{
    internal interface ICountryService 
    {
        City? GetCity(int cityId);
        Task<City?> GetCityAsync(int cityId, CancellationToken token);
        Country? GetCountry(int countryId);
        Task<Country?> GetCountryAsync(int countryId, CancellationToken token);
        IEnumerable<City> GetAllCities();
        Task<IEnumerable<City>> GetAllCitiesAsync(CancellationToken token);
        IEnumerable<Country> GetAllCountries();
        Task<IEnumerable<Country>> GetAllCountriesAsync(CancellationToken token);
        IEnumerable<City> GetCitiesByCountry(int countryId);
        Task<IEnumerable<City>> GetCitiesByCountryAsync(int countryId, CancellationToken token);
    }
}
