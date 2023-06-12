
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
    public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
    {
        IEnumerable<Customer> customersFromDatabase = await _customerRepository.GetCustomersAsync();
        IEnumerable<CustomerDto> customersToReturn = _mapper.Map<IEnumerable<CustomerDto>>(customersFromDatabase);
        return Ok(customersToReturn);
    }

    [HttpGet("{id}", Name = "GetCustomerById")]
    public ActionResult<CustomerDto> GetCustomerById(int id)
    {
        Customer? customerFromDatabase = _customerRepository.GetCustomerById(id);
        if (customerFromDatabase == null) return NotFound();

        CustomerDto customerToReturn = _mapper.Map<CustomerDto>(customerFromDatabase);

        return Ok(customerToReturn);
    }


    [HttpGet("cpf/{cpf}")]
    public ActionResult<CustomerDto> GetCustomerByCpf(string cpf)
    {
        Customer? customerFromDatabase = _customerRepository.GetCustomerByCpf(cpf);
        if (customerFromDatabase == null) return NotFound();

        CustomerDto customerToReturn = _mapper.Map<CustomerDto>(customerFromDatabase);

        return Ok(customerToReturn);
    }

    [HttpPost]
    public ActionResult<CustomerDto> CreateCustomer(CustomerForCreationDto customerForCreationDto) {

        Customer customerEntity = _mapper.Map<Customer>(customerForCreationDto);
        _customerRepository.CreateCustomer(customerEntity);

        CustomerDto customerToReturn = _mapper.Map<CustomerDto>(customerEntity);
        return CreatedAtRoute
        (
            "GetCustomerById",
            new { id = customerToReturn.Id },
            customerToReturn
        );
    }

    [HttpPut("{id}")]
    public ActionResult UpdateCustomer(int id, CustomerForUpdateDto customerForUpdateDto)
    {
        if (id != customerForUpdateDto.Id) return BadRequest();

        Customer? customerFromDatabase = _customerRepository.GetCustomerById(id);
        if (customerFromDatabase == null) return NotFound();

        _customerRepository.UpdateCustomer(customerFromDatabase, customerForUpdateDto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCustomer(int id)
    {
        Customer? customerFromDatabase = _customerRepository.GetCustomerById(id);
        if (customerFromDatabase == null) return NotFound();

        _customerRepository.DeleteCustomer(customerFromDatabase);

        return NoContent();
    }

    [HttpPatch("{id}")]
    public ActionResult PartiallyUpdateCustomer(
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

    [HttpGet("with-addresses")]
    public ActionResult<IEnumerable<CustomerWithAddressesDto>> GetCustomersWithAddresses()
    {
        var customersFromDatabase = _context.Customers;

        IEnumerable<CustomerWithAddressesDto> customersToReturn = _mapper.Map<IEnumerable<CustomerWithAddressesDto>>(customersFromDatabase);
        foreach(CustomerWithAddressesDto c in customersToReturn) {
            c.Addresses = _mapper.Map<ICollection<AddressDto>>(_context.Addresses.ToList().FindAll(a => a.CustomerId == c.Id));
        }

        return Ok(customersToReturn);
    }

    [HttpGet("with-addresses/{id}", Name = "GetCustomerWithAddressesById")]
    public ActionResult<IEnumerable<CustomerWithAddressesDto>> GetCustomerWithAddressesById(int id)
    {
        Customer? customerFromDatabase = _context.Customers.FirstOrDefault(c => c.Id == id);
        if (customerFromDatabase == null) return NotFound();

        CustomerWithAddressesDto customerToReturn = _mapper.Map<CustomerWithAddressesDto>(customerFromDatabase);
        customerToReturn.Addresses = _mapper.Map<ICollection<AddressDto>>(_context.Addresses.ToList().FindAll(a => a.CustomerId == customerToReturn.Id));

        return Ok(customerToReturn);
    }

    [HttpPost("with-addresses")]
    public ActionResult<CustomerWithAddressesDto> CreateCustomerWithAddresses(
        CustomerForCreationWithAddressesDto customerForCreationWithAddressesDto)
    {
        Customer customerEntity = _mapper.Map<Customer>(customerForCreationWithAddressesDto);

        _context.Customers.Add(customerEntity);
        _context.SaveChanges();

        CustomerWithAddressesDto customerToReturn = _mapper.Map<CustomerWithAddressesDto>(customerEntity);

        return CreatedAtRoute
        (
            "GetCustomerWithAddressesById",
            new { id = customerToReturn.Id },
            customerToReturn
        );
    }

    [HttpPut("with-addresses/{id}")]
    public ActionResult UpdateCustomerWithAddress(int id,
        CustomerForUpdateWithAddressesDto customerForUpdateWithAddressesDto)
    {
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