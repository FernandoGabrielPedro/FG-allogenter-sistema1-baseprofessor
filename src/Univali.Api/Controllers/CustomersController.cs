
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Univali.Api.DbContexts;
using Univali.Api.Entities;
using Univali.Api.Models;

namespace Univali.Api.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : MainController
{
    private readonly Data _data;
    private readonly IMapper _mapper;
    private readonly CustomerContext _context;
    public CustomersController (Data data, IMapper mapper, CustomerContext context) {
        _data = data ?? throw new ArgumentNullException(nameof(data));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet]
    public ActionResult<IEnumerable<CustomerDto>> GetCustomers()
    {
        List<Customer> customersFromDatabase = _context.Customers.OrderBy(c => c.Id).ToList();
        IEnumerable<CustomerDto> customersToReturn = _mapper.Map<IEnumerable<CustomerDto>>(customersFromDatabase);
        return Ok(customersToReturn);
    }

    [HttpGet("{id}", Name = "GetCustomerById")]
    public ActionResult<CustomerDto> GetCustomerById(int id)
    {
        Customer? customerFromDatabase = _context
            .Customers.FirstOrDefault(c => c.Id == id);

        if (customerFromDatabase == null) return NotFound();

        CustomerDto customerToReturn = _mapper.Map<CustomerDto>(customerFromDatabase);

        return Ok(customerToReturn);
    }


    [HttpGet("cpf/{cpf}")]
    public ActionResult<CustomerDto> GetCustomerByCpf(string cpf)
    {
        var customerFromDatabase = _context.Customers
            .FirstOrDefault(c => c.Cpf == cpf);

        if (customerFromDatabase == null) return NotFound();

        CustomerDto customerToReturn = _mapper.Map<CustomerDto>(customerFromDatabase);

        return Ok(customerToReturn);
    }

    [HttpPost]
    public ActionResult<CustomerDto> CreateCustomer(CustomerForCreationDto customerForCreationDto) {

        Customer customerEntity = _mapper.Map<Customer>(customerForCreationDto);
        customerEntity.Id = _context.Customers.Max(c => c.Id) + 1;

        _context.Customers.Add(customerEntity);
        _context.SaveChanges();

        CustomerDto customerToReturn = _mapper.Map<CustomerDto>(customerEntity);

        return CreatedAtRoute
        (
            "GetCustomerById",
            new { id = customerToReturn.Id },
            customerToReturn
        );
    }

    [HttpPut("{id}")]
    public ActionResult UpdateCustomer(int id,
        CustomerForUpdateDto customerForUpdateDto)
    {
        if (id != customerForUpdateDto.Id) return BadRequest();

        var customerFromDatabase = _data.Customers
            .FirstOrDefault(customer => customer.Id == id);

        if (customerFromDatabase == null) return NotFound();

        _mapper.Map(customerForUpdateDto, customerFromDatabase);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCustomer(int id)
    {
        var customerFromDatabase = _data.Customers
            .FirstOrDefault(customer => customer.Id == id);

        if (customerFromDatabase == null) return NotFound();

        _data.Customers.Remove(customerFromDatabase);

        return NoContent();
    }

    [HttpPatch("{id}")]
    public ActionResult PartiallyUpdateCustomer(
        [FromBody] JsonPatchDocument<CustomerForPatchDto> patchDocument,
        [FromRoute] int id)
    {
        Customer? customerFromDatabase = _data.Customers
            .FirstOrDefault(customer => customer.Id == id);

        if (customerFromDatabase == null) return NotFound();

        CustomerForPatchDto customerToPatch = _mapper.Map<CustomerForPatchDto>(customerFromDatabase);

        patchDocument.ApplyTo(customerToPatch);

        if(!TryValidateModel(customerToPatch))
            return ValidationProblem(ModelState);

        customerFromDatabase.Name = customerToPatch.Name;
        customerFromDatabase.Cpf = customerToPatch.Cpf;

        return NoContent();

    }

    [HttpGet("with-addresses")]
    public ActionResult<IEnumerable<CustomerWithAddressesDto>> GetCustomersWithAddresses()
    {
        var customersFromDatabase = _data.Customers;

        IEnumerable<CustomerWithAddressesDto> customersToReturn = _mapper.Map<IEnumerable<CustomerWithAddressesDto>>(customersFromDatabase);

        return Ok(customersToReturn);
    }

    [HttpGet("{id}/with-addresses", Name = "GetCustomerWithAddressesById")]
    public ActionResult<IEnumerable<CustomerWithAddressesDto>> GetCustomerWithAddressesById(int id)
    {
        Customer? customerFromDatabase = _data.Customers.FirstOrDefault(c => c.Id == id);
        if (customerFromDatabase == null) return NotFound();

        CustomerWithAddressesDto customerToReturn = _mapper.Map<CustomerWithAddressesDto>(customerFromDatabase);

        return Ok(customerToReturn);
    }

    [HttpPost("with-addresses")]
    public ActionResult<CustomerWithAddressesDto> CreateCustomerWithAddresses(
        CustomerForCreationWithAddressesDto customerForCreationWithAddressesDto)
    {
        int idMax = _data.Customers.SelectMany(c => c.Addresses).Max(a => a.Id) + 1;
        Customer customerEntity = _mapper.Map<Customer>(customerForCreationWithAddressesDto);
        customerEntity.Addresses = customerForCreationWithAddressesDto.Addresses.Select(a => {
            Address newAddress =  _mapper.Map<Address>(a);
            newAddress.Id = idMax++;
            return newAddress;
        }).ToList();

        _data.Customers.Add(customerEntity);

        CustomerWithAddressesDto customerToReturn = _mapper.Map<CustomerWithAddressesDto>(customerForCreationWithAddressesDto);

        return CreatedAtRoute
        (
            "GetCustomerWithAddressesById",
            new { id = customerToReturn.Id },
            customerToReturn
        );
    }

    [HttpPut("{id}/with-addresses")]
    public ActionResult UpdateCustomerWithAddress(int id,
        CustomerForUpdateWithAddressesDto customerForUpdateWithAddressesDto)
    {
        if (id != customerForUpdateWithAddressesDto.Id) return BadRequest();

        Customer? customerFromDatabase = _data.Customers
            .FirstOrDefault(customer => customer.Id == id);
        if (customerFromDatabase == null) return NotFound();

        customerFromDatabase = _mapper.Map<Customer>(customerForUpdateWithAddressesDto);

        int idMax = _data.Customers.SelectMany(c => c.Addresses).Max(a => a.Id) + 1;
        customerFromDatabase.Addresses = customerForUpdateWithAddressesDto.Addresses.Select(a => {
            Address newAddress =  _mapper.Map<Address>(a);
            newAddress.Id = idMax++;
            return newAddress;
        }).ToList();

        return NoContent();
    }
}