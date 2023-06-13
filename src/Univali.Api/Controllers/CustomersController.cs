
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Univali.Api.DbContexts;
using Univali.Api.Entities;
using Univali.Api.Features.Customers.Commands.CreateCustomer;
using Univali.Api.Features.Customers.Commands.DeleteCustomer;
using Univali.Api.Features.Customers.Commands.UpdateCustomer;
using Univali.Api.Features.Customers.Queries.GetCustomerDetail;
using Univali.Api.Features.Customers.Queries.GetCustomerDetailByCpf;
using Univali.Api.Features.Customers.Queries.GetCustomersDetail;
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
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomersAsync([FromServices] IGetCustomersDetailQueryHandler handler)
    {
        GetCustomersDetailQuerie getCustomersDetailQuerie = new GetCustomersDetailQuerie();
        IEnumerable<GetCustomersDetailDto?> customersToReturn = await handler.Handle(getCustomersDetailQuerie);

        return Ok(customersToReturn);
    }

    [HttpGet("{id}", Name = "GetCustomerById")]
    public async Task<ActionResult<CustomerDto>> GetCustomerByIdAsync([FromServices] IGetCustomerDetailQueryHandler handler, int id)
    {
        GetCustomerDetailQuerie getCustomerDetailQuery = new GetCustomerDetailQuerie {Id = id};
        GetCustomerDetailDto? customerToReturn = await handler.Handle(getCustomerDetailQuery);

        if (customerToReturn == null) return NotFound();

        return Ok(customerToReturn);
    }


    [HttpGet("cpf/{cpf}")]
    public async Task<ActionResult<CustomerDto>> GetCustomerByCpfAsync([FromServices] IGetCustomerDetailByCpfQueryHandler handler, string cpf)
    {
        GetCustomerDetailByCpfQuerie getCustomerDetailByCpfQuery = new GetCustomerDetailByCpfQuerie {Cpf = cpf};
        GetCustomerDetailByCpfDto? customerToReturn = await handler.Handle(getCustomerDetailByCpfQuery);

        if (customerToReturn == null) return NotFound();

        return Ok(customerToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDto>> CreateCustomerAsync([FromServices] ICreateCustomerCommandHandler handler, CreateCustomerCommand createCustomerCommand) {

        CreateCustomerDto? customerToReturn = await handler.Handle(createCustomerCommand);
        
        return CreatedAtRoute
        (
            "GetCustomerById",
            new { id = customerToReturn.Id },
            customerToReturn
        );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCustomerAsync([FromServices] IUpdateCustomerCommandHandler handler, UpdateCustomerCommand updateCustomerCommand, int id)
    {
        if (id != updateCustomerCommand.Id) return BadRequest();

        bool result = await handler.Handle(updateCustomerCommand, id);
        if(!result) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCustomerAsync([FromServices] IDeleteCustomerCommandHandler handler, int id)
    {
        DeleteCustomerCommand deleteCustomerCommand = new DeleteCustomerCommand {Id = id};
        bool result = await handler.Handle(deleteCustomerCommand);
        if(!result) return NotFound();

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
    public async Task<ActionResult<IEnumerable<CustomerWithAddressesDto>>> GetCustomersWithAddressesAsync()
    {
        IEnumerable<Customer> customersFromDatabase = await _customerRepository.GetCustomersWithAddressesAsync();

        IEnumerable<CustomerWithAddressesDto> customersToReturn = _mapper.Map<IEnumerable<CustomerWithAddressesDto>>(customersFromDatabase);
        foreach(CustomerWithAddressesDto c in customersToReturn) {
            c.Addresses = _mapper.Map<ICollection<AddressDto>>(_customerRepository.GetAddressesByCustomerIdAsync(c.Id).Result);
        }

        return Ok(customersToReturn);
    }

    [HttpGet("with-addresses/{id}", Name = "GetCustomerWithAddressesById")]
    public async Task<ActionResult<IEnumerable<CustomerWithAddressesDto>>> GetCustomerWithAddressesByIdAsync(int id)
    {
        Customer? customerFromDatabase = await _customerRepository.GetCustomerByIdAsync(id);
        if (customerFromDatabase == null) return NotFound();

        CustomerWithAddressesDto customerToReturn = _mapper.Map<CustomerWithAddressesDto>(customerFromDatabase);
        customerToReturn.Addresses = _mapper.Map<ICollection<AddressDto>>(_customerRepository.GetAddressesByCustomerIdAsync(id).Result);

        return Ok(customerToReturn);
    }

    [HttpPost("with-addresses")]
    public async Task<ActionResult<CustomerWithAddressesDto>> CreateCustomerWithAddressesAsync(
        CustomerForCreationWithAddressesDto customerForCreationWithAddressesDto)
    {
        Customer customerEntity = _mapper.Map<Customer>(customerForCreationWithAddressesDto);

        _customerRepository.CreateCustomer(customerEntity);
        await _customerRepository.SaveChangesAsync();

        CustomerWithAddressesDto customerToReturn = _mapper.Map<CustomerWithAddressesDto>(customerEntity);

        return CreatedAtRoute
        (
            "GetCustomerWithAddressesById",
            new { id = customerToReturn.Id },
            customerToReturn
        );
    }

    //ARRUMAR
    [HttpPut("with-addresses/{id}")]
    public async Task<ActionResult> UpdateCustomerWithAddress(int id,
        CustomerForUpdateWithAddressesDto customerForUpdateWithAddressesDto)
    {
        if (id != customerForUpdateWithAddressesDto.Id) return BadRequest();

        Customer? customerFromDatabase = _context.Customers.Include(c => c.Addresses)
            .FirstOrDefault(customer => customer.Id == id);
        if (customerFromDatabase == null) return NotFound();

        var updatedCustomer = _mapper.Map<Customer>(customerForUpdateWithAddressesDto);
        _mapper.Map(updatedCustomer, customerFromDatabase);
        await _customerRepository.SaveChangesAsync();

        return NoContent();
    }
}