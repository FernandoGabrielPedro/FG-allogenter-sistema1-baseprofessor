namespace Univali.Api.Features.CustomersWithAddresses.Queries.GetCustomersWithAddressesDetail;

public class CustomerForGetCustomersWithAddressesDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public ICollection<AddressForGetCustomersWithAddressesDetailDto> Addresses { get; set; } = new List<AddressForGetCustomersWithAddressesDetailDto>();
}
