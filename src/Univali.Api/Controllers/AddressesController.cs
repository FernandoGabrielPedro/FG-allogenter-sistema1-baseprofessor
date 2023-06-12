using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Univali.Api.DbContexts;
using Univali.Api.Entities;
using Univali.Api.Models;
using Univali.Api.Repositories;

namespace Univali.Api.Controllers;

[ApiController]
[Route("api/addresses")]
public class AddressesController : MainController
{
    private readonly Data _data;
    private readonly IMapper _mapper;
    private readonly CustomerContext _context;
    private readonly ICustomerRepository _customerRepository;
    public AddressesController (Data data, IMapper mapper, CustomerContext context, ICustomerRepository customerRepository) {
        _data = data ?? throw new ArgumentNullException(nameof(data));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AddressDto>>> GetAddressesAsync() {
        IEnumerable<Address> addressesFromDatabase = await _customerRepository.GetAddressesAsync();
        IEnumerable<AddressDto> addressesToReturn = _mapper.Map<IEnumerable<AddressDto>>(addressesFromDatabase);
        return Ok(addressesToReturn);
    }

    [HttpGet("{id}", Name = "GetAddressById")]
    public async Task<ActionResult<AddressDto>> GetAddressByIdAsync(int id) {
        Address? addressFromDatabase = await _customerRepository.GetAddressByIdAsync(id);
        if(addressFromDatabase == null) return NotFound();
        
        AddressDto addressToReturn = _mapper.Map<AddressDto>(addressFromDatabase);
        return Ok(addressToReturn);
    }

    [HttpGet("/customerId/{customerId}")]
    public async Task<ActionResult<IEnumerable<AddressDto>>> GetAddressesByCustomerIdAsync(int customerId) {
        IEnumerable<Address> addressesFromDatabase = await _customerRepository.GetAddressesByCustomerIdAsync(customerId);
        IEnumerable<AddressDto> addressesToReturn = _mapper.Map<IEnumerable<AddressDto>>(addressesFromDatabase);
        return Ok(addressesToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<AddressDto>> CreateAddressAsync([FromBody] AddressForCreationDto addressForCreationDto) {
        Address addressEntity = _mapper.Map<Address>(addressForCreationDto);
        addressEntity.Id = _context.Addresses.Max(c => c.Id) + 1;

        _customerRepository.CreateAddress(addressEntity);
        await _customerRepository.SaveChangesAsync();

        AddressDto addressToReturn = _mapper.Map<AddressDto>(addressEntity);
        return CreatedAtRoute
        (
            "GetAddressById",
            new { id = addressToReturn.Id },
            addressToReturn
        );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AddressDto>> UpdateAddressAsync(int id, AddressForUpdateDto addressForUpdateDto) {
        if (addressForUpdateDto.Id != id) return BadRequest();

        Address? addressFromDatabase = await _customerRepository.GetAddressByIdAsync(id);
        if (addressFromDatabase == null) return NotFound();

        _mapper.Map(addressForUpdateDto, addressFromDatabase);
        await _customerRepository.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<AddressDto>> DeleteAddressAsync(int id) {
        Address? addressFromDatabase = await _customerRepository.GetAddressByIdAsync(id);
        if(addressFromDatabase == null) return NotFound();

        _customerRepository.DeleteAddress(addressFromDatabase);
        await _customerRepository.SaveChangesAsync();

        return NoContent();
    }
}
