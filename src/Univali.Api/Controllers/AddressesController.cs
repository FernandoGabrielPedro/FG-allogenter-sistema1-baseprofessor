using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Univali.Api.DbContexts;
using Univali.Api.Entities;
using Univali.Api.Features.Addresses.Commands.CreateAddress;
using Univali.Api.Features.Addresses.Commands.DeleteAddress;
using Univali.Api.Features.Addresses.Commands.UpdateAddress;
using Univali.Api.Features.Addresses.Queries.GetAddressDetail;
using Univali.Api.Features.Addresses.Queries.GetAddressesDetail;
using Univali.Api.Features.Addresses.Queries.GetAddressesDetailByCustomerId;
using Univali.Api.Models;
using Univali.Api.Repositories;

namespace Univali.Api.Controllers;

[ApiController]
[Route("api/addresses")]
public class AddressesController : MainController
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMediator _mediator;
    public AddressesController (IMapper mapper, ICustomerRepository customerRepository, IMediator mediator) {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AddressDto>>> GetAddressesAsync() {
        GetAddressesDetailQuery getAddressesDetailQuery = new GetAddressesDetailQuery();
        IEnumerable<GetAddressesDetailDto> addressesToReturn = await _mediator.Send(getAddressesDetailQuery);
        return Ok(addressesToReturn);
    }

    [HttpGet("{id}", Name = "GetAddressById")]
    public async Task<ActionResult<AddressDto>> GetAddressByIdAsync(int id) {
        GetAddressDetailQuery getAddressDetailQuery = new GetAddressDetailQuery {Id = id};
        GetAddressDetailDto? addressToReturn = await _mediator.Send(getAddressDetailQuery);
        return Ok(addressToReturn);
    }

    [HttpGet("customerId/{customerId}")]
    public async Task<ActionResult<IEnumerable<AddressDto>>> GetAddressesByCustomerIdAsync(int customerId) {
        GetAddressesDetailByCustomerIdQuery getAddressesDetailByCustomerIdQuery = new GetAddressesDetailByCustomerIdQuery {CustomerId = customerId};
        IEnumerable<GetAddressesDetailByCustomerIdDto> addressesToReturn = await _mediator.Send(getAddressesDetailByCustomerIdQuery);
        return Ok(addressesToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<CreateAddressDto>> CreateAddressAsync(CreateAddressCommand createAddressCommand) {

        CreateAddressDto addressToReturn = await _mediator.Send(createAddressCommand);

        return CreatedAtRoute
        (
            "GetAddressById",
            new { id = addressToReturn.Id },
            addressToReturn
        );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAddressAsync(UpdateAddressCommand updateAddressCommand, int id) {
        if (updateAddressCommand.Id != id) return BadRequest();

        bool result = await _mediator.Send(updateAddressCommand);
        if(!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAddressAsync(int id) {
        DeleteAddressCommand deleteAddressCommand = new DeleteAddressCommand {Id = id};
        bool result = await _mediator.Send(deleteAddressCommand);
        if(!result) return NotFound();
        return NoContent();
    }
}