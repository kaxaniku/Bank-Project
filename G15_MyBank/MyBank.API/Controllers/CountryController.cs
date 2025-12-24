using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBank.Application.Interfaces.Services;

namespace MyBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries(CancellationToken token, int pageNumber, int pageSize)
        {
            return Ok(await _countryService.GetAllCountriesAsync(token, pageNumber, pageSize));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCountry(int countryId, CancellationToken token)
        {
            return Ok(await _countryService.GetCountryAsync(countryId, token));
        }

        [HttpGet("cityBy{Id}")]
        public async Task<IActionResult> GetCitiesByCountry(int countryId, CancellationToken token, int pageNumber, int pageSize)
        {
            return Ok(await _countryService.GetCitiesByCountryAsync(countryId, token, pageNumber, pageSize));
        }
    }
}
