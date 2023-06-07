using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Univali.Api.Entities;
using Univali.Api.Models;

namespace Univali.Api.Controllers;

[ApiController]
[Route("api/customers/{customerId}/addresses")]
public class AddressesController : MainController
{
    private readonly Data _data;
    private readonly IMapper _mapper;
    public AddressesController (Data data, IMapper mapper) {
        _data = data ?? throw new ArgumentNullException(nameof(data));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public ActionResult<IEnumerable<AddressDto>> GetAddresses(int customerId)
    {
        var customerFromDatabase = _data.Customers
            .FirstOrDefault(customer => customer.Id == customerId);

        if (customerFromDatabase == null) return NotFound();

        var addressesToReturn = new List<AddressDto>();

        foreach (var address in customerFromDatabase.Addresses)
        {
            addressesToReturn.Add(_mapper.Map<AddressDto>(address));
        }

        return Ok(addressesToReturn);
    }


    [HttpGet("{addressId}", Name = "GetAddressById")]
    public ActionResult<AddressDto> GetAddressById(int customerId, int addressId)
    {
        var addressFromDatabase = _data
            .Customers.FirstOrDefault(customer => customer.Id == customerId)
            ?.Addresses.FirstOrDefault(address => address.Id == addressId);
        
        AddressDto addressToReturn = _mapper.Map<AddressDto>(addressFromDatabase);

        return addressToReturn != null ? Ok(addressToReturn) : NotFound();
    }

    [HttpPost]
    public ActionResult<AddressDto> CreateAddress([FromRoute] int customerId, [FromBody] AddressForCreationDto addressForCreationDto) {
        var customerFromDatabase = _data.Customers
            .FirstOrDefault(customer => customer.Id == customerId);

        if (customerFromDatabase == null) return NotFound();

        Address addressEntity = _mapper.Map<Address>(addressForCreationDto);

        customerFromDatabase.Addresses.Add(addressEntity);

        AddressDto addressToReturn = _mapper.Map<AddressDto>(addressEntity);

        return CreatedAtRoute
        (
            "GetAddressById",
            new { customerId = customerId, addressId = addressToReturn.Id },
            addressToReturn
        );
    }

    [HttpPut("{addressId}")]
    public ActionResult<AddressDto> UpdateAddress(int customerId, int addressId, AddressForEditionDto addressForEditionDto) {
        if (addressForEditionDto.Id != addressId) return BadRequest();

        Address? addressFromCustomer = _data.
            Customers.FirstOrDefault(customer => customer.Id == customerId)
            ?.Addresses.FirstOrDefault(address => address.Id == addressId);
        if (addressFromCustomer == null) return NotFound();

        addressFromCustomer = _mapper.Map<Address>(addressForEditionDto);

        return NoContent();
    }

    [HttpDelete("{addressId}")]
    public ActionResult<AddressDto> DeleteAddress(int customerId, int addressId) {
        Customer? customerFromDatabase = _data.Customers
            .FirstOrDefault(customer => customer.Id == customerId);
        if (customerFromDatabase == null) return NotFound();

        Address? addressFromCustomer = customerFromDatabase.Addresses
            .FirstOrDefault(address => address.Id == addressId);
        if(addressFromCustomer == null) return NotFound();

        customerFromDatabase.Addresses.Remove(addressFromCustomer);

        return NoContent();
    }
}
