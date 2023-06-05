using Microsoft.AspNetCore.Mvc;
using Univali.Api.Entities;
using Univali.Api.Models;

namespace Univali.Api.Controllers;

[ApiController]
[Route("api/customers/{customerId}/addresses")]
public class AddressController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<AddressDto>> GetAddresses(int customerId)
    {
        var customerFromDatabase = Data.Instance.Customers
            .FirstOrDefault(customer => customer.Id == customerId);

        if (customerFromDatabase == null) return NotFound();

        var addressesToReturn = new List<AddressDto>();

        foreach (var address in customerFromDatabase.Addresses)
        {
            addressesToReturn.Add(new AddressDto
            {
                Id = address.Id,
                Street = address.Street,
                City = address.City
            });
        }

        return Ok(addressesToReturn);
    }


    [HttpGet("{addressId}", Name = "GetAddressById")]
    public ActionResult<AddressDto> GetAddressById(int customerId, int addressId)
    {
        var addressToReturn = Data.Instance
            .Customers.FirstOrDefault(customer => customer.Id == customerId)
            ?.Addresses.FirstOrDefault(address => address.Id == addressId);

        return addressToReturn != null ? Ok(addressToReturn) : NotFound();
    }

    [HttpPost]
    public ActionResult<AddressDto> CreateAddress([FromRoute] int customerId, [FromBody] AddressForCreationDto addressForCreationDto) {
        var customerFromDatabase = Data.Instance.Customers
            .FirstOrDefault(customer => customer.Id == customerId);

        if (customerFromDatabase == null) return NotFound();

        Address addressEntity = new Address {
            Id = Data.Instance.Customers.SelectMany(c => c.Addresses).Max(a => a.Id) + 1,
            Street = addressForCreationDto.Street,
            City = addressForCreationDto.City
        };

        customerFromDatabase.Addresses.Add(addressEntity);

        AddressDto addressToReturn = new AddressDto {
            Id = addressEntity.Id,
            Street = addressEntity.Street,
            City = addressEntity.City,
        };

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

        Address? addressFromCustomer = Data.Instance.
            Customers.FirstOrDefault(customer => customer.Id == customerId)
            ?.Addresses.FirstOrDefault(address => address.Id == addressId);
        if (addressFromCustomer == null) return NotFound();

        addressFromCustomer.Street = addressForEditionDto.Street;
        addressFromCustomer.City = addressForEditionDto.City;

        return NoContent();
    }

    [HttpDelete("{addressId}")]
    public ActionResult<AddressDto> DeleteAddress(int customerId, int addressId) {
        Customer? customerFromDatabase = Data.Instance.Customers
            .FirstOrDefault(customer => customer.Id == customerId);
        if (customerFromDatabase == null) return NotFound();

        Address? addressFromCustomer = customerFromDatabase.Addresses
            .FirstOrDefault(address => address.Id == addressId);
        if(addressFromCustomer == null) return NotFound();

        customerFromDatabase.Addresses.Remove(addressFromCustomer);

        return NoContent();
    }
}
