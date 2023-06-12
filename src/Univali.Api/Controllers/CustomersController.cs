
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Univali.Api.DbContexts;
using Univali.Api.Entities;
using Univali.Api.Models;
using Univali.Api.Repositories;

namespace Univali.Api.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : MainController
{
    private readonly Data _data;
    private readonly IMapper _mapper;
    private readonly CustomerContext _context;
    private readonly ICustomerRepository _customerRepository;
    public CustomersController (Data data, IMapper mapper, CustomerContext context, ICustomerRepository customerRepository) {
        _data = data ?? throw new ArgumentNullException(nameof(data));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomersAsync()
    {
        IEnumerable<Customer> customersFromDatabase = await _customerRepository.GetCustomersAsync();
        IEnumerable<CustomerDto> customersToReturn = _mapper.Map<IEnumerable<CustomerDto>>(customersFromDatabase);
        return Ok(customersToReturn);
    }

    [HttpGet("{id}", Name = "GetCustomerById")]
    public async Task<ActionResult<CustomerDto>> GetCustomerByIdAsync(int id)
    {
        Customer? customerFromDatabase = await _customerRepository.GetCustomerByIdAsync(id);
        if (customerFromDatabase == null) return NotFound();

        CustomerDto customerToReturn = _mapper.Map<CustomerDto>(customerFromDatabase);

        return Ok(customerToReturn);
    }


    [HttpGet("cpf/{cpf}")]
    public async Task<ActionResult<CustomerDto>> GetCustomerByCpfAsync(string cpf)
    {
        Customer? customerFromDatabase = await _customerRepository.GetCustomerByCpfAsync(cpf);
        if (customerFromDatabase == null) return NotFound();

        CustomerDto customerToReturn = _mapper.Map<CustomerDto>(customerFromDatabase);

        return Ok(customerToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDto>> CreateCustomerAsync(CustomerForCreationDto customerForCreationDto) {

        Customer customerEntity = _mapper.Map<Customer>(customerForCreationDto);
        _customerRepository.CreateCustomerAsync(customerEntity);

        CustomerDto customerToReturn = _mapper.Map<CustomerDto>(customerEntity);
        return CreatedAtRoute
        (
            "GetCustomerById",
            new { id = customerToReturn.Id },
            customerToReturn
        );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCustomerAsync(int id, CustomerForUpdateDto customerForUpdateDto)
    {
        if (id != customerForUpdateDto.Id) return BadRequest();

        Customer? customerFromDatabase = await _customerRepository.GetCustomerByIdAsync(id);
        if (customerFromDatabase == null) return NotFound();

        _customerRepository.UpdateCustomerAsync(customerFromDatabase, customerForUpdateDto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCustomerAsync(int id)
    {
        Customer? customerFromDatabase = await _customerRepository.GetCustomerByIdAsync(id);
        if (customerFromDatabase == null) return NotFound();

        _customerRepository.DeleteCustomerAsync(customerFromDatabase);

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> PartiallyUpdateCustomerAsync(
        [FromBody] JsonPatchDocument<CustomerForPatchDto> patchDocument,
        [FromRoute] int id)
    {
        Customer? customerFromDatabase = _context.Customers
            .FirstOrDefault(customer => customer.Id == id);

        if (customerFromDatabase == null) return NotFound();

        CustomerForPatchDto customerToPatch = _mapper.Map<CustomerForPatchDto>(customerFromDatabase);

        patchDocument.ApplyTo(customerToPatch);

        if(!TryValidateModel(customerToPatch))
            return ValidationProblem(ModelState);

        customerFromDatabase.Name = customerToPatch.Name;
        customerFromDatabase.Cpf = customerToPatch.Cpf;
        _context.SaveChanges();

        return NoContent();

    }

    //Arrumar Async With-Addresses

    [HttpGet("with-addresses")]
    public async Task<ActionResult<IEnumerable<CustomerWithAddressesDto>>> GetCustomersWithAddresses()
    {
        IEnumerable<Customer> customersFromDatabase = await _customerRepository.GetCustomersAsync();

        IEnumerable<CustomerWithAddressesDto> customersToReturn = _mapper.Map<IEnumerable<CustomerWithAddressesDto>>(customersFromDatabase);
        foreach(CustomerWithAddressesDto c in customersToReturn) {
            c.Addresses = _mapper.Map<ICollection<AddressDto>>(_customerRepository.GetAddressesByCustomerIdAsync(c.Id));
        }

        return Ok(customersToReturn);
    }

    [HttpGet("with-addresses/{id}", Name = "GetCustomerWithAddressesById")]
    public async Task<ActionResult<IEnumerable<CustomerWithAddressesDto>>> GetCustomerWithAddressesById(int id)
    {
        Customer? customerFromDatabase = await _customerRepository.GetCustomerByIdAsync(id);
        if (customerFromDatabase == null) return NotFound();

        CustomerWithAddressesDto customerToReturn = _mapper.Map<CustomerWithAddressesDto>(customerFromDatabase);
        customerToReturn.Addresses = _mapper.Map<ICollection<AddressDto>>(_customerRepository.GetAddressesByCustomerIdAsync(id));

        return Ok(customerToReturn);
    }

    [HttpPost("with-addresses")]
    public async Task<ActionResult<CustomerWithAddressesDto>> CreateCustomerWithAddressesAsync(
        CustomerForCreationWithAddressesDto customerForCreationWithAddressesDto)
    {
        Customer customerEntity = _mapper.Map<Customer>(customerForCreationWithAddressesDto);

        _customerRepository.CreateCustomerAsync(customerEntity);

        CustomerWithAddressesDto customerToReturn = _mapper.Map<CustomerWithAddressesDto>(customerEntity);

        return CreatedAtRoute
        (
            "GetCustomerWithAddressesById",
            new { id = customerToReturn.Id },
            customerToReturn
        );
    }

    [HttpPut("with-addresses/{id}")]
    public async Task<ActionResult> UpdateCustomerWithAddress(int id,
        CustomerForUpdateWithAddressesDto customerForUpdateWithAddressesDto)
    {
        //Arrumar Async

        if (id != customerForUpdateWithAddressesDto.Id) return BadRequest();

        Customer? customerFromDatabase = _context.Customers.Include(c => c.Addresses)
            .FirstOrDefault(customer => customer.Id == id);
        if (customerFromDatabase == null) return NotFound();

        var updatedCustomer = _mapper.Map<Customer>(customerForUpdateWithAddressesDto);
        _mapper.Map(updatedCustomer, customerFromDatabase);
        _context.SaveChanges();

        return NoContent();
    }
}