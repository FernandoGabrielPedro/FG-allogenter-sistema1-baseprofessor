namespace Univali.Api.Features.Addresses.Queries.GetAddressesDetailByCustomerId;

public class GetAddressesDetailByCustomerIdDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}
