
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
using Univali.Api.Features.CustomersWithAddresses.Commands.CreateCustomerWithAddresses;
using Univali.Api.Features.CustomersWithAddresses.Commands.UpdateCustomerWithAddresses;
using Univali.Api.Features.CustomersWithAddresses.Queries.GetCustomersWithAddressesDetail;
using Univali.Api.Features.CustomersWithAddresses.Queries.GetCustomerWithAddressesDetail;
using Univali.Api.Models;
using Univali.Api.Repositories;

namespace Univali.Api.Controllers;

[ApiController]
[Route("api/customers")]
[Authorize]
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
        [FromBody] JsonPatchDocument<PatchCustomerDto> patchDocument,
        [FromRoute] int id)
    {
        PatchCustomerCommand patchCustomerCommand = new PatchCustomerCommand {PatchDocument = patchDocument, Id = id};
        PatchCustomerReturnDto? customerToPatch = await _mediator.Send(patchCustomerCommand);

        if(customerToPatch == null) return NotFound();
        if(!TryValidateModel(customerToPatch))
            return ValidationProblem(ModelState);
        
        UpdateCustomerCommand updateCustomerCommand = _mapper.Map<UpdateCustomerCommand>(customerToPatch);
        await _mediator.Send(updateCustomerCommand);

        return NoContent();
    }

    [HttpGet("with-addresses")]
    public async Task<ActionResult<IEnumerable<CustomerForGetCustomersWithAddressesDetailDto>>> GetCustomersWithAddressesAsync()
    {
        GetCustomersWithAddressesDetailQuery getCustomersWithAddressesDetailQuery = new GetCustomersWithAddressesDetailQuery();
        IEnumerable<CustomerForGetCustomersWithAddressesDetailDto> customersToReturn = await _mediator.Send(getCustomersWithAddressesDetailQuery);

        return Ok(customersToReturn);
    }

    [HttpGet("with-addresses/{id}", Name = "GetCustomerWithAddressesById")]
    public async Task<ActionResult<IEnumerable<CustomerWithAddressesDto>>> GetCustomerWithAddressesByIdAsync(int id)
    {
        GetCustomerWithAddressesDetailQuery getCustomerWithAddressesDetailQuery = new GetCustomerWithAddressesDetailQuery {Id = id};
        CustomerForGetCustomerWithAddressesDetailDto customerToReturn = await _mediator.Send(getCustomerWithAddressesDetailQuery);

        return Ok(customerToReturn);
    }

    [HttpPost("with-addresses")]
    public async Task<ActionResult<CustomerToReturnForCreateCustomerWithAddressesDto>> CreateCustomerWithAddressesAsync(
        CreateCustomerWithAddressesCommand createCustomerWithAddressesCommand)
    {
        CustomerToReturnForCreateCustomerWithAddressesDto customerToReturn = await _mediator.Send(createCustomerWithAddressesCommand);

        return CreatedAtRoute
        (
            "GetCustomerWithAddressesById",
            new { id = customerToReturn.Id },
            customerToReturn
        );
    }

    [HttpPut("with-addresses/{id}")]
    public async Task<ActionResult> UpdateCustomerWithAddress(int id,
        UpdateCustomerWithAddressesCommand updateCustomerWithAddressesCommand)
    {
        if (id != updateCustomerWithAddressesCommand.Id) return BadRequest();

        bool result = await _mediator.Send(updateCustomerWithAddressesCommand);

        if(!result) return NotFound();
        return NoContent();
    }
}