namespace Univali.Api.Models;

public class CustomerForUpdateWithAddressesDto
{
    public int Id {get; set;}
    public string Name {get; set;} = string.Empty;
    public string Cpf {get; set;} = string.Empty;
    public ICollection<AddressForUpdateDto> Addresses { get; set; }
     = new List<AddressForUpdateDto>();
}