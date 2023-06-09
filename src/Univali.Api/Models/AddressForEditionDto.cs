namespace Univali.Api.Models;

public class AddressForEditionDto
{
    public int Id {get; set;}
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public int CustomerId {get; set;}
}