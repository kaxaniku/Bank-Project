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

    public Country GetCountry(int countryId)
    {
        Country customer = _unitOfWork.CountryRepository.GetById(countryId)
            ?? throw new InvalidOperationException($"Country with ID {countryId} does not exist.");
        if (!customer.Activity.IsActive)
            throw new InvalidOperationException($"Country with ID {countryId} no longer exists.");
        return customer;
    }

    // TODO: Add pagination support
    public IEnumerable<City> ListAllCities()
    {
        return _unitOfWork.CityRepository.Query(x => x.Activity.IsActive);
    }

    public City GetCity(int cityId)
    {
        City city = _unitOfWork.CityRepository.GetById(cityId)
            ?? throw new InvalidOperationException($"City with ID {cityId} does not exist.");
        if (!city.Activity.IsActive)
            throw new InvalidOperationException($"City with ID {cityId} no longer exists.");
        return city;
    }

    // TODO: Add pagination support
    public IEnumerable<City> ListCitiesByCountry(int countryId)
    {
        return _unitOfWork.CityRepository.Query(x => x.Country.CountryId == countryId, x => x.Country);
    }

    public IAsyncEnumerable<Country> ListAllCountriesAsync(CancellationToken cancellationToken)
    {
        return _unitOfWork.CountryRepository.QueryAsync(x => x.Activity.IsActive, cancellationToken);
    }

    public async Task<Country> GetCountryAsync(int countryId, CancellationToken cancellationToken)
    {
        Country customer = await _unitOfWork.CountryRepository.GetByIdAsync(countryId, cancellationToken)
            ?? throw new InvalidOperationException($"Country with ID {countryId} does not exist.");
        if (!customer.Activity.IsActive)
            throw new InvalidOperationException($"Country with ID {countryId} no longer exists.");
        return customer;
    }

    // TODO: Add pagination support
    public IAsyncEnumerable<City> ListAllCitiesAsync(CancellationToken cancellationToken)
    {
        return _unitOfWork.CityRepository.QueryAsync(x => x.Activity.IsActive, cancellationToken);
    }

    public async Task<City> GetCityAsync(int cityId, CancellationToken cancellationToken)
    {
        City city = await _unitOfWork.CityRepository.GetByIdAsync(cityId, cancellationToken)
            ?? throw new InvalidOperationException($"City with ID {cityId} does not exist.");
        if (!city.Activity.IsActive)
            throw new InvalidOperationException($"City with ID {cityId} no longer exists.");
        return city;
    }

    // TODO: Add pagination support
    public IAsyncEnumerable<City> ListCitiesByCountryAsync(int countryId, CancellationToken cancellationToken)
    {
        return _unitOfWork.CityRepository.QueryAsync(x => x.Country.CountryId == countryId, cancellationToken, x => x.Country);
    }
}
