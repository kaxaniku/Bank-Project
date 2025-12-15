using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBank.API.Models;
using MyBank.Application.Interfaces.Services;

namespace MyBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        [HttpGet("{personalNumber}")]
        public async Task<IActionResult> GetCustomer(string personalNumber, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers(string personalNumber, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCostumer([FromBody] CustomerModel customer, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CustomerModel customer, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{personalNumber}")]
        public async Task<IActionResult> Remove(string personalNumber, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
