namespace Univali.Api.Features.CustomersWithAddresses.Commands.UpdateCustomerWithAddresses;

public class AddressToUpdateForUpdateCustomerWithAddressesDto
{
    public int Id {get; set;}
    public string Street { get; set; } = String.Empty;
    public string City { get; set; } = String.Empty;
}