
using AutoMapper;
using MediatR;
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
using Univali.Api.Features.Customers.Commands.PatchCustomer;
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
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMediator _mediator;
    public CustomersController (IMapper mapper, ICustomerRepository customerRepository, IMediator mediator) {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomersAsync()
    {
        GetCustomersDetailQuery getCustomersDetailQuery = new GetCustomersDetailQuery();
        IEnumerable<GetCustomersDetailDto?> customersToReturn = await _mediator.Send(getCustomersDetailQuery);

        return Ok(customersToReturn);
    }

    [HttpGet("{id}", Name = "GetCustomerById")]
    public async Task<ActionResult<CustomerDto>> GetCustomerByIdAsync(int id)
    {
        GetCustomerDetailQuery getCustomerDetailQuery = new GetCustomerDetailQuery {Id = id};
        GetCustomerDetailDto? customerToReturn = await _mediator.Send(getCustomerDetailQuery);

        if (customerToReturn == null) return NotFound();

        return Ok(customerToReturn);
    }


    [HttpGet("cpf/{cpf}")]
    public async Task<ActionResult<CustomerDto>> GetCustomerByCpfAsync(string cpf)
    {
        GetCustomerDetailByCpfQuery getCustomerDetailByCpfQuery = new GetCustomerDetailByCpfQuery {Cpf = cpf};
        GetCustomerDetailByCpfDto? customerToReturn = await _mediator.Send(getCustomerDetailByCpfQuery);

        if (customerToReturn == null) return NotFound();

        return Ok(customerToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<CreateCustomerDto>> CreateCustomerAsync(CreateCustomerCommand createCustomerCommand) {

        CreateCustomerDto customerToReturn = await _mediator.Send(createCustomerCommand);
        
        return CreatedAtRoute
        (
            "GetCustomerById",
            new { id = customerToReturn.Id },
            customerToReturn
        );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCustomerAsync(UpdateCustomerCommand updateCustomerCommand, int id)
    {
        if (id != updateCustomerCommand.Id) return BadRequest();

        bool result = await _mediator.Send(updateCustomerCommand);
        if(!result) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCustomerAsync(int id)
    {
        DeleteCustomerCommand deleteCustomerCommand = new DeleteCustomerCommand {Id = id};
        bool result = await _mediator.Send(deleteCustomerCommand);
        if(!result) return NotFound();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> PartiallyUpdateCustomerAsync(
        [FromBody] JsonPatchDocument<CustomerForPatchDto> patchDocument,
        [FromRoute] int id)
    {
        Customer? customerFromDatabase = await _customerRepository.GetCustomerByIdAsync(id);
        if (customerFromDatabase == null) return NotFound();

        CustomerForPatchDto customerToPatch = _mapper.Map<CustomerForPatchDto>(customerFromDatabase);

        patchDocument.ApplyTo(customerToPatch);

        if(!TryValidateModel(customerToPatch))
            return ValidationProblem(ModelState);

        customerFromDatabase.Name = customerToPatch.Name;
        customerFromDatabase.Cpf = customerToPatch.Cpf;
        await _customerRepository.SaveChangesAsync();

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
        Customer? customerFromDatabase = await _customerRepository.GetCustomerWithAddressesByIdAsync(id);
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

        Customer? customerFromDatabase = await _customerRepository.GetCustomerWithAddressesByIdAsync(id);
        if (customerFromDatabase == null) return NotFound();

        var updatedCustomer = _mapper.Map<Customer>(customerForUpdateWithAddressesDto);
        _mapper.Map(updatedCustomer, customerFromDatabase);
        await _customerRepository.SaveChangesAsync();

        return NoContent();
    }
}