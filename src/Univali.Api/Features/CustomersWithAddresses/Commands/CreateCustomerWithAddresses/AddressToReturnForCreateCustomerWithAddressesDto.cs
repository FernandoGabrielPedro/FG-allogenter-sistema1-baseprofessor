namespace Univali.Api.Features.CustomersWithAddresses.Commands.CreateCustomerWithAddresses;

public class AddressToReturnForCreateCustomerWithAddressesDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}