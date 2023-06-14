namespace Univali.Api.Features.CustomersWithAddresses.Queries.GetCustomerWithAddressesDetail;

public class AddressForGetCustomerWithAddressesDetailDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}
