using Microsoft.AspNetCore.Mvc;
using MyBank.Application.Interfaces.Services;


namespace MyBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICountryService _cityService;

        public CityController(ICountryService cityService)
        {
            _cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
        }

        [HttpGet]
        public async Task<IActionResult> GetCities(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity([FromRoute] int id, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
