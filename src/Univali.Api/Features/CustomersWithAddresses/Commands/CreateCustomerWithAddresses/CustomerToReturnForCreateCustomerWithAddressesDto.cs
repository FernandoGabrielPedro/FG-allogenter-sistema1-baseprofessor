namespace Univali.Api.Features.CustomersWithAddresses.Commands.CreateCustomerWithAddresses;

public class CustomerToReturnForCreateCustomerWithAddressesDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public ICollection<AddressToReturnForCreateCustomerWithAddressesDto> Addresses { get; set; } = new List<AddressToReturnForCreateCustomerWithAddressesDto>();
}