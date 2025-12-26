using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBank.API.Models;
using MyBank.API.Models.CustomerModels;
using MyBank.Application.Interfaces.Services;

namespace MyBank.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;

    public CustomerController(ICustomerService customerService, IMapper mapper)
    {
        _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        _mapper = mapper;
    }

    [HttpGet("{customerId}")]
    public async Task<IActionResult> FindCustomer([FromRoute] int customerId, CancellationToken cancellationToken)
    {
        if (customerId <= 0)
            return BadRequest("customerId is required");
        var customer = await _customerService.FindCustomerAsync(customerId, cancellationToken);
        var customerModel = _mapper.Map<CustomerModel>(customer);
        return Ok(customerModel);
    }

    // TODO: Move this to the AccountController
    [HttpGet("list/{customerId}")]
    public async Task<IActionResult> GetAccountsByCustomer([FromRoute] int customerId, CancellationToken cancellationToken)
    {
        if (customerId <= 0)
            return BadRequest("customerId is required");
        var accounts = await _customerService.ListAccountsByCustomerAsync(customerId, cancellationToken);
        var accountModels = _mapper.Map<IEnumerable<AccountModel>>(accounts);
        return Ok(accountModels);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterCustomer([FromBody] CustomerModel? customer, CancellationToken cancellationToken)
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
                customer.AddressLine1,
                customer.AddressLine2,
                customer.ZipCode,
                customer.CityId,
                cancellationToken);
        return Ok(new { customer, message = "Customer successfully registered." });
    }

    [HttpDelete("{customerId}")]
    public async Task<IActionResult> RemoveCustomer(int customerId, CancellationToken cancellationToken)
    {
        if (customerId <= 0)
            return BadRequest("customerId is required");
        await _customerService.RemoveCustomerAsync(customerId, cancellationToken);
        return NoContent();
    }

    [HttpPut("updateDisplayInfo/{customerId}")]
    public async Task<IActionResult> UpdateCustomerDisplayInfo(int customerId, [FromBody] CustomerDisplayModel? customer, CancellationToken cancellationToken)
    {
        if (customer == null)
            return BadRequest("Customer is null");
        await _customerService.UpdateCustomerDisplayInfoAsync(
            customerId,
            customer.FirstName,
            customer.LastName,
            customer.Gender,
            customer.DateOfBirth,
            cancellationToken);
        return Ok(new { customer, message = "Customer display information successfully updated." });
    }

    [HttpPut("updatePrivateInfo/{customerId}")]
    public async Task<IActionResult> UpdateCustomerPrivateInfo(int customerId, [FromBody] CustomerPrivateModel? customer, CancellationToken cancellationToken)
    {
        if (customer == null)
            return BadRequest("Customer is null");
        await _customerService.UpdateCustomerPrivateInfoAsync(
            customerId,
            customer.PersonalNumber,
            customer.Email,
            customer.PhoneNumber,
            cancellationToken);
        return Ok(new { customer, message = "Customer private information successfully updated." });
    }

    [HttpPut("updateAddressInfo/{customerId}")]
    public async Task<IActionResult> UpdateCustomerAddressInfo(int customerId, [FromBody] CustomerAddressModel? customer, CancellationToken cancellationToken)
    {
        if (customer == null)
            return BadRequest("Customer is null");
        await _customerService.UpdateCustomerAddressInfoAsync(
            customerId,
            customer.AddressLine1,
            customer.AddressLine2,
            customer.ZipCode,
            customer.CityId,
            cancellationToken);
        return Ok(new { customer, message = "Customer address information successfully updated." });
    }
}