using MyBank.Application.Interfaces.Repositories;
using MyBank.Application.Interfaces.Services;
using MyBank.Domain;

namespace MyBank.Application
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<City> GetAllCities()
        {
            return _unitOfWork.CityRepository.Query(c => c.Activity.IsActive);
        }

        public async Task<IEnumerable<City>> GetAllCitiesAsync(CancellationToken token)
        {
            return await _unitOfWork.CityRepository.QueryAsync(c => c.Activity.IsActive, token);
        }

        public IEnumerable<Country> GetAllCountries()
        {
            return _unitOfWork.CountryRepository.Query(c => c.Activity.IsActive);
        }

        public async Task<IEnumerable<Country>> GetAllCountriesAsync(CancellationToken token)
        {
            return await _unitOfWork.CountryRepository.QueryAsync(c => c.Activity.IsActive, token);
        }

        public IEnumerable<City> GetCitiesByCountry(int countryId)
        {
            return _unitOfWork.CityRepository.Query(c => c.Country.CountryId == countryId).ToList();
        }

        public async Task<IEnumerable<City>> GetCitiesByCountryAsync(int countryId, CancellationToken token)
        {
            return await _unitOfWork.CityRepository.QueryAsync(c => c.Country.CountryId == countryId, token);
        }

        public City? GetCity(int cityId)
        {
            return _unitOfWork.CityRepository.GetById(cityId);
        }

        public async Task<City?> GetCityAsync(int cityId, CancellationToken token)
        {
            return await _unitOfWork.CityRepository.GetByIdAsync(cityId, token);
        }

        public Country? GetCountry(int countryId)
        {
            return _unitOfWork.CountryRepository.GetById(countryId);
        }

        public async Task<Country?> GetCountryAsync(int countryId, CancellationToken token)
        {
            return await _unitOfWork.CountryRepository.GetByIdAsync(countryId, token);
        }
    }
}
