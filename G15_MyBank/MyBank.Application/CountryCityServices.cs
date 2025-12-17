using Microsoft.EntityFrameworkCore;
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

    public IEnumerable<Country> ListAllCountries(int pageSize = 10, int pageNumber = 1)
    {
        if (pageNumber < 1)
            throw new ArgumentException("Page number must be >= 1.", nameof(pageNumber));

        return _unitOfWork.CountryRepository
            .Query(x => x.Activity.IsActive, pageNumber, pageSize)
            .ToList();
    }

    public Country GetCountry(int countryId)
    {
        Country country = _unitOfWork.CountryRepository.GetById(countryId)
            ?? throw new InvalidOperationException($"Country with ID {countryId} does not exist.");
        if (!country.Activity.IsActive)
            throw new InvalidOperationException($"Country with ID {countryId} no longer exists.");
        return country;
    }

    public IEnumerable<City> ListAllCities(int pageSize = 10, int pageNumber = 1)
    {
        if (pageNumber < 1)
            throw new ArgumentException("Page number must be >= 1.", nameof(pageNumber));

        return _unitOfWork.CityRepository
            .Query(x => x.Activity.IsActive, pageNumber, pageSize)
            .ToList();
    }

    public City GetCity(int cityId)
    {
        City city = _unitOfWork.CityRepository.GetById(cityId)
            ?? throw new InvalidOperationException($"City with ID {cityId} does not exist.");
        if (!city.Activity.IsActive)
            throw new InvalidOperationException($"City with ID {cityId} no longer exists.");
        return city;
    }

    public IEnumerable<City> ListCitiesByCountry(int countryId, int pageSize = 10, int pageNumber = 1)
    {
        if (pageNumber < 1)
            throw new ArgumentException("Page number must be >= 1.", nameof(pageNumber));

        return _unitOfWork.CityRepository
            .Query(x => x.Country.CountryId == countryId, pageNumber, pageSize, x => x.Country)
            .ToList();
    }

    public async Task<IEnumerable<Country>> ListAllCountriesAsync(CancellationToken cancellationToken, int pageSize = 10, int pageNumber = 1)
    {
        if (pageNumber < 1)
            throw new ArgumentException("Page number must be >= 1.", nameof(pageNumber));

        return await _unitOfWork.CountryRepository
            .QueryAsync(x => x.Activity.IsActive, pageNumber, pageSize, cancellationToken);
    }

    public async Task<Country> GetCountryAsync(int countryId, CancellationToken cancellationToken)
    {
        Country customer = await _unitOfWork.CountryRepository.GetByIdAsync(countryId, cancellationToken)
            ?? throw new InvalidOperationException($"Country with ID {countryId} does not exist.");
        if (!customer.Activity.IsActive)
            throw new InvalidOperationException($"Country with ID {countryId} no longer exists.");
        return customer;
    }

    public async Task<IEnumerable<City>> ListAllCitiesAsync(CancellationToken cancellationToken, int pageSize = 10, int pageNumber = 1)
    {
        if (pageNumber < 1)
            throw new ArgumentException("Page number must be >= 1.", nameof(pageNumber));

        return await _unitOfWork.CityRepository
            .QueryAsync(x => x.Activity.IsActive, pageNumber, pageSize, cancellationToken);
    }

    public async Task<City> GetCityAsync(int cityId, CancellationToken cancellationToken)
    {
        City city = await _unitOfWork.CityRepository.GetByIdAsync(cityId, cancellationToken)
            ?? throw new InvalidOperationException($"City with ID {cityId} does not exist.");
        if (!city.Activity.IsActive)
            throw new InvalidOperationException($"City with ID {cityId} no longer exists.");
        return city;
    }

    public async Task<IEnumerable<City>> ListCitiesByCountryAsync(int countryId, CancellationToken cancellationToken, int pageSize = 10, int pageNumber = 1)
    {
        if (pageNumber < 1)
            throw new ArgumentException("Page number must be >= 1.", nameof(pageNumber));

        return await _unitOfWork.CityRepository
            .QueryAsync(x => x.Country.CountryId == countryId, pageNumber, pageSize, cancellationToken, x => x.Country);
    }
}
