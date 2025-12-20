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

        public IEnumerable<City> GetAllCities(int pageNumber, int pageSize = 10)
        {
            if (pageNumber < 1)
                throw new ArgumentException("Page number must be greater or equal of 1", nameof(pageNumber));

            return _unitOfWork.CityRepository.Query(c => c.Activity.IsActive, pageNumber, pageSize).ToList();
        }

        public async Task<IEnumerable<City>> GetAllCitiesAsync(CancellationToken token, int pageNumber, int pageSize = 10)
        {
            if (pageNumber < 1)
                throw new ArgumentException("Page number must be greater or equal of 1", nameof(pageNumber));

            var query = await _unitOfWork.CityRepository.QueryAsync(c => c.Activity.IsActive, token, pageNumber, pageSize);

            return query.ToList();
        }

        public IEnumerable<Country> GetAllCountries(int pageNumber, int pageSize = 10)
        {
            if (pageNumber < 1)
                throw new ArgumentException("Page number must be greater or equal of 1", nameof(pageNumber));

            return _unitOfWork.CountryRepository.Query(c => c.Activity.IsActive, pageNumber, pageSize).ToList();
        }

        public async Task<IEnumerable<Country>> GetAllCountriesAsync(CancellationToken token, int pageNumber, int pageSize = 10)
        {
            if (pageNumber < 1)
                throw new ArgumentException("Page number must be greater or equal of 1", nameof(pageNumber));

            var query = await _unitOfWork.CountryRepository.QueryAsync(c => c.Activity.IsActive, token, pageNumber, pageSize);

            return query.ToList();
        }

        public IEnumerable<City> GetCitiesByCountry(int countryId, int pageNumber, int pageSize = 10)
        {
            if (pageNumber < 1)
                throw new ArgumentException("Page number must be greater or equal of 1", nameof(pageNumber));

            return _unitOfWork.CityRepository.Query(c => c.Country.CountryId == countryId, pageNumber, pageSize).ToList();
        }

        public async Task<IEnumerable<City>> GetCitiesByCountryAsync(int countryId, CancellationToken token, int pageNumber, int pageSize = 10)
        {
            if (pageNumber < 1)
                throw new ArgumentException("Page number must be greater or equal of 1", nameof(pageNumber));

            var query = await _unitOfWork.CityRepository.QueryAsync(c => c.Country.CountryId == countryId, token, pageNumber, pageSize);

            return query.ToList();
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
