namespace Univali.Api.Features.CustomersWithAddresses.Queries.GetCustomerWithAddressesDetail;

public class CustomerForGetCustomerWithAddressesDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public ICollection<AddressForGetCustomerWithAddressesDetailDto> Addresses { get; set; } = new List<AddressForGetCustomerWithAddressesDetailDto>();
}
