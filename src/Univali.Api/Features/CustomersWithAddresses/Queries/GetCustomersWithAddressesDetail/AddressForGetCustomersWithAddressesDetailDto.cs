namespace Univali.Api.Features.CustomersWithAddresses.Queries.GetCustomersWithAddressesDetail;

public class AddressForGetCustomersWithAddressesDetailDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}
