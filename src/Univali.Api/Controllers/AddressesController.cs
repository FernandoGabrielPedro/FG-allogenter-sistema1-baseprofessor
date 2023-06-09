using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Univali.Api.DbContexts;
using Univali.Api.Entities;
using Univali.Api.Models;

namespace Univali.Api.Controllers;

[ApiController]
[Route("api/addresses")]
public class AddressesController : MainController
{
    private readonly Data _data;
    private readonly IMapper _mapper;
    private readonly CustomerContext _context;
    public AddressesController (Data data, IMapper mapper, CustomerContext context) {
        _data = data ?? throw new ArgumentNullException(nameof(data));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet]
    public ActionResult<IEnumerable<AddressDto>> GetAddresses()
    {
        var addressesFromDatabase = _context.Addresses.ToList();
        var addressesToReturn = _mapper.Map<IEnumerable<AddressDto>>(addressesFromDatabase);
        return Ok(addressesToReturn);
    }


    [HttpGet("{addressId}", Name = "GetAddressById")]
    public ActionResult<AddressDto> GetAddressById(int addressId)
    {
        var addressFromDatabase = _context.Addresses.FirstOrDefault(address => address.Id == addressId);
        if(addressFromDatabase == null) return NotFound();
        
        AddressDto addressToReturn = _mapper.Map<AddressDto>(addressFromDatabase);

        return Ok(addressToReturn);
    }

    [HttpPost]
    public ActionResult<AddressDto> CreateAddress([FromBody] AddressForCreationDto addressForCreationDto) {

        Address addressEntity = _mapper.Map<Address>(addressForCreationDto);

        _context.Addresses.Add(addressEntity);
        _context.SaveChanges();

        AddressDto addressToReturn = _mapper.Map<AddressDto>(addressEntity);

        return CreatedAtRoute
        (
            "GetAddressById",
            new { addressId = addressToReturn.Id },
            addressToReturn
        );
    }

    [HttpPut("{addressId}")]
    public ActionResult<AddressDto> UpdateAddress(int addressId, AddressForEditionDto addressForEditionDto) {
        if (addressForEditionDto.Id != addressId) return BadRequest();

        Address? addressFromDatabase = _context.Addresses.FirstOrDefault(address => address.Id == addressId);
        if (addressFromDatabase == null) return NotFound();

        addressFromDatabase = _mapper.Map<Address>(addressForEditionDto);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{addressId}")]
    public ActionResult<AddressDto> DeleteAddress(int customerId, int addressId) {
        Address? addressFromDatabase = _context.Addresses
            .FirstOrDefault(address => address.Id == addressId);
        if(addressFromDatabase == null) return NotFound();

        _context.Addresses.Remove(addressFromDatabase);

        return NoContent();
    }
}
