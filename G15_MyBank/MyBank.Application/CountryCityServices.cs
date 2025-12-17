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

        if (pageSize < 1)
            throw new ArgumentException("Page size must be >= 1.", nameof(pageSize));

        return _unitOfWork.CountryRepository.ListActive(pageNumber, pageSize);
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

        if (pageSize < 1)
            throw new ArgumentException("Page size must be >= 1.", nameof(pageSize));

        return _unitOfWork.CityRepository.ListActive(pageNumber, pageSize);
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

        int skip = (pageNumber - 1) * pageSize;

        IQueryable<City> query =
            _unitOfWork.CityRepository
                .Query(x => x.Country.CountryId == countryId, x => x.Country);

        return query
            .Skip(skip)
            .Take(pageSize)
            .ToList();
    }

    public async Task<IEnumerable<Country>> ListAllCountriesAsync(CancellationToken cancellationToken, int pageSize = 10, int pageNumber = 1)
    {
        if (pageNumber < 1)
            throw new ArgumentException("Page number must be >= 1.", nameof(pageNumber));

        int skip = (pageNumber - 1) * pageSize;

        IQueryable<Country> query =
            _unitOfWork.CountryRepository
                .Query(x => x.Activity.IsActive);

        return await query
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
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

        int skip = (pageNumber - 1) * pageSize;

        IQueryable<City> query =
            _unitOfWork.CityRepository
                .Query(x => x.Activity.IsActive);

        return await query
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
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

        int skip = (pageNumber - 1) * pageSize;

        IQueryable<City> query =
            _unitOfWork.CityRepository
                .Query(x => x.Country.CountryId == countryId, x => x.Country);

        return await query
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}
