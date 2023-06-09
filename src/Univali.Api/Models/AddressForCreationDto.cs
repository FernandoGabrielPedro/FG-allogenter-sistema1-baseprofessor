namespace Univali.Api.Models;

public class AddressForCreationDto
{
    public int CustomerId {get; set;}
    public string Street { get; set; } = String.Empty;
    public string City { get; set; } = String.Empty;
}