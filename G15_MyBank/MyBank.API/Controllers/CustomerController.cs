using Microsoft.AspNetCore.Mvc;
using MyBank.API.Models;
using MyBank.Application.Interfaces.Services;

namespace MyBank.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        [HttpGet("{customerNumber}")]
        public async Task<IActionResult> FindCustomer([FromRoute] int customerId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{accountNumber}/list")]
        public async Task<IActionResult> GetAccountsByCustomer([FromRoute] int customerId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCustomer([FromBody] CustomerModel customer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("remove/{customerNumber}")]
        public async Task<IActionResult> RemoveCustomer(int customerId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{customerNumber}/updateDisplayInfo")]
        public async Task<IActionResult> UpdateCustomerDisplayInfo([FromBody] CustomerModel customer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{customerNumber}/updatePrivateInfo")]
        public async Task<IActionResult> UpdateCustomerPrivateInfo([FromBody] CustomerModel customer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{customerNumber}/updateAddressInfo")]
        public async Task<IActionResult> UpdateCustomerAddressInfo([FromBody] CustomerModel customer, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}