
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Univali.Api.Entities;
using Univali.Api.Models;

namespace Univali.Api.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly Data _data;
    private readonly IMapper _mapper;
    public CustomersController (Data data, IMapper mapper) {
        _data = data ?? throw new ArgumentNullException(nameof(data));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public ActionResult<IEnumerable<CustomerDto>> GetCustomers()
    {
        var customersFromDatabase = _data.Customers;
        var customersToReturn = _mapper.Map<IEnumerable<CustomerDto>>(customersFromDatabase);
        return Ok(customersToReturn);
    }

    [HttpGet("{id}", Name = "GetCustomerById")]
    public ActionResult<CustomerDto> GetCustomerById(int id)
    {
        var customerFromDatabase = _data
            .Customers.FirstOrDefault(c => c.Id == id);

        if (customerFromDatabase == null) return NotFound();

        CustomerDto customerToReturn = new CustomerDto
        {
            Id = customerFromDatabase.Id,
            Name = customerFromDatabase.Name,
            Cpf = customerFromDatabase.Cpf
        };
        return Ok(customerToReturn);
    }


    [HttpGet("cpf/{cpf}")]
    public ActionResult<CustomerDto> GetCustomerByCpf(string cpf)
    {
        var customerFromDatabase = _data.Customers
            .FirstOrDefault(c => c.Cpf == cpf);

        if (customerFromDatabase == null)
        {
            return NotFound();
        }

        CustomerDto customerToReturn = new CustomerDto
        {
            Id = customerFromDatabase.Id,
            Name = customerFromDatabase.Name,
            Cpf = customerFromDatabase.Cpf
        };
        return Ok(customerToReturn);
    }

    [HttpPost]
    public ActionResult<CustomerDto> CreateCustomer(
        CustomerForCreationDto customerForCreationDto)
    {


        if (!ModelState.IsValid)
        {
            Response.ContentType = "application/problem+json";
            // Cria a fábrica de um objeto de detalhes de problema de validação
            var problemDetailsFactory = HttpContext.RequestServices
                .GetRequiredService<ProblemDetailsFactory>();

            // Cria um objeto de detalhes de problema de validação
            var validationProblemDetails = problemDetailsFactory
                .CreateValidationProblemDetails(HttpContext, ModelState);

            // Atribui o status code 422 no corpo do response
            validationProblemDetails.Status = StatusCodes.Status422UnprocessableEntity;

            return UnprocessableEntity(validationProblemDetails);
        }

        var customerEntity = new Customer()
        {
            Id = _data.Customers.Max(c => c.Id) + 1,
            Name = customerForCreationDto.Name,
            Cpf = customerForCreationDto.Cpf
        };

        _data.Customers.Add(customerEntity);

        var customerToReturn = new CustomerDto
        {
            Id = customerEntity.Id,
            Name = customerForCreationDto.Name,
            Cpf = customerForCreationDto.Cpf
        };

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
        var customerFromDatabase = _data.Customers
            .FirstOrDefault(customer => customer.Id == id);

        if (customerFromDatabase == null) return NotFound();

        var customerToPatch = new CustomerForPatchDto
        {
            Name = customerFromDatabase.Name,
            Cpf = customerFromDatabase.Cpf
        };

        patchDocument.ApplyTo(customerToPatch);

        customerFromDatabase.Name = customerToPatch.Name;
        customerFromDatabase.Cpf = customerToPatch.Cpf;

        return NoContent();

    }

    [HttpGet("with-addresses")]
    public ActionResult<IEnumerable<CustomerWithAddressesDto>> GetCustomersWithAddresses()
    {
        var customersFromDatabase = _data.Customers;

        var customersToReturn = customersFromDatabase
            .Select(customer => new CustomerWithAddressesDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Cpf = customer.Cpf,
                Addresses = customer.Addresses
                    .Select(address => new AddressDto
                    {
                        Id = address.Id,
                        City = address.City,
                        Street = address.Street
                    }).ToList()
            });

        return Ok(customersToReturn);
    }

    [HttpGet("{id}/with-addresses", Name = "GetCustomerWithAddressesById")]
    public ActionResult<IEnumerable<CustomerWithAddressesDto>> GetCustomerWithAddressesById(int id)
    {
        Customer? customerFromDatabase = _data.Customers.FirstOrDefault(c => c.Id == id);
        if (customerFromDatabase == null) return NotFound();

        CustomerWithAddressesDto customerToReturn = new CustomerWithAddressesDto
        {
            Id = customerFromDatabase.Id,
            Name = customerFromDatabase.Name,
            Cpf = customerFromDatabase.Cpf,
            Addresses = customerFromDatabase.Addresses
                    .Select(address => new AddressDto
                    {
                        Id = address.Id,
                        City = address.City,
                        Street = address.Street
                    }).ToList()
        };

        return Ok(customerToReturn);
    }

    [HttpPost("with-addresses")]
    public ActionResult<CustomerWithAddressesDto> CreateCustomerWithAddresses(
        CustomerForCreationWithAddressesDto customerForCreationWithAddressesDto)
    {
        int idMax = _data.Customers.SelectMany(c => c.Addresses).Max(a => a.Id) + 1;
        var customerEntity = new Customer
        {
            Id = _data.Customers.Max(c => c.Id) + 1,
            Name = customerForCreationWithAddressesDto.Name,
            Cpf = customerForCreationWithAddressesDto.Cpf,
            Addresses = customerForCreationWithAddressesDto.Addresses.Select(a =>
                new Address
                {
                    Id = idMax++,
                    Street = a.Street,
                    City = a.City
                }
            ).ToList()
        };

        _data.Customers.Add(customerEntity);

        var customerToReturn = new CustomerWithAddressesDto
        {
            Id = customerEntity.Id,
            Name = customerEntity.Name,
            Cpf = customerEntity.Cpf,
            Addresses = customerEntity.Addresses.Select(a =>
            new AddressDto
            {
                Id = a.Id,
                Street = a.Street,
                City = a.City
            }).ToList()
        };

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

        customerFromDatabase.Name = customerForUpdateWithAddressesDto.Name;
        customerFromDatabase.Cpf = customerForUpdateWithAddressesDto.Cpf;
        int idMax = _data.Customers.SelectMany(c => c.Addresses).Max(a => a.Id) + 1;
        customerFromDatabase.Addresses = customerForUpdateWithAddressesDto.Addresses.Select(a =>
            new Address
            {
                Id = idMax++,
                Street = a.Street,
                City = a.City
            }
        ).ToList();

        return NoContent();
    }
}