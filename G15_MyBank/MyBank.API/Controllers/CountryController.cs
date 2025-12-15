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

        [HttpGet("countries/{id}")]
        public async Task<IActionResult> GetCountries(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{countryId}")]
        public async Task<IActionResult> GetCountry(int countryId, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{countryId}/cities")]
        public async Task<IActionResult> GetCitiesByCountry(int countryId, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
