using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
            if (personalNumber == null)
                return BadRequest("Personal number is null");

            return Ok(await _customerService.FindCustomerAsync(personalNumber, token));
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers(int pageNumber, int pageSize, CancellationToken token)
        {
            return Ok(await _customerService.GetAllCustomersAsync(token, pageNumber, pageSize));
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCostumer([FromBody] CustomerModel customer, CancellationToken token)
        {
            if (customer == null)
                return BadRequest("Customer is null");

            await _customerService.RegisterNewCustomerAsync(
                customer.PersonalNumber,
                customer.FirstName,
                customer.LastName,
                customer.Gender,
                customer.Email,
                customer.PhoneNumber,
                customer.DateOfBirth,
                customer.AdressLine1,
                customer.AdressLine2,
                customer.ZipCode,
                customer.City.CityId,
                token);

            return Ok(customer);
        }

        [HttpPut("update/{personalNumber}")]
        public async Task<IActionResult> Update(string personalNumber, CancellationToken token)
        {
            if (personalNumber == null)
                return BadRequest("Personal number is null");

            await _customerService.UpdateCustomerAsync(personalNumber, token);

            return Ok("Customer updated sucesfully");
        }

        [HttpDelete("{personalNumber}")]
        public async Task<IActionResult> Remove(string personalNumber, CancellationToken token)
        {
            if (personalNumber == null)
                return BadRequest("Personal number is null");

            await _customerService.RemoveCustomerAsync(personalNumber, token);

            return Ok("Customer removed sucesfully");
        }
    }
}
