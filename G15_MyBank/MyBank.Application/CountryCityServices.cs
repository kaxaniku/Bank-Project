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
        return _unitOfWork.CountryRepository.Query(x => x.Activity.IsActive);
    }

    public Country? GetCountry(int countryId)
    {
        return _unitOfWork.CountryRepository.GetById(countryId);
    }

    public IEnumerable<City> ListAllCities()
    {
        return _unitOfWork.CityRepository.Query(x => x.Activity.IsActive);
    }

    public City? GetCity(int cityId)
    {
        return _unitOfWork.CityRepository.GetById(cityId);
    }

    public IEnumerable<City> ListCitiesByCountry(int countryId)
    {
        return _unitOfWork.CityRepository.Query(x => x.Country.CountryId == countryId, x => x.Country);
    }

    public async Task<IEnumerable<Country>> ListAllCountriesAsync(CancellationToken cancellationToken)
    {
        return await _unitOfWork.CountryRepository.QueryAsync(x => x.Activity.IsActive, cancellationToken);
    }

    public async Task<Country?> GetCountryAsync(int countryId, CancellationToken cancellationToken)
    {
        return await _unitOfWork.CountryRepository.GetByIdAsync(countryId, cancellationToken);
    }

    public async Task<IEnumerable<City>> ListAllCitiesAsync(CancellationToken cancellationToken)
    {
        return await _unitOfWork.CityRepository.QueryAsync(x => x.Activity.IsActive, cancellationToken);
    }

    public async Task<City?> GetCityAsync(int cityId, CancellationToken cancellationToken)
    {
        return await _unitOfWork.CityRepository.GetByIdAsync(cityId, cancellationToken);
    }

    public async Task<IEnumerable<City>> ListCitiesByCountryAsync(int countryId, CancellationToken cancellationToken)
    {
        return await _unitOfWork.CityRepository.QueryAsync(x => x.Country.CountryId == countryId, cancellationToken, x => x.Country);
    }
}
