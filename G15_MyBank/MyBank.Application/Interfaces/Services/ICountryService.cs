using MyBank.Domain;

namespace MyBank.Application.Interfaces.Services
{
    public interface ICountryService 
    {
        City? GetCity(int cityId);
        Task<City?> GetCityAsync(int cityId, CancellationToken token);
        Country? GetCountry(int countryId);
        Task<Country?> GetCountryAsync(int countryId, CancellationToken token);
        IEnumerable<City> GetAllCities(int pageNumber, int pageSize);
        Task<IEnumerable<City>> GetAllCitiesAsync(CancellationToken token, int pageNumber, int pageSize);
        IEnumerable<Country> GetAllCountries(int pageNumber, int pageSize);
        Task<IEnumerable<Country>> GetAllCountriesAsync(CancellationToken token, int pageNumber, int pageSize);
        IEnumerable<City> GetCitiesByCountry(int countryId, int pageNumber, int pageSize);
        Task<IEnumerable<City>> GetCitiesByCountryAsync(int countryId, CancellationToken token, int pageNumber, int pageSize);
    }
}
