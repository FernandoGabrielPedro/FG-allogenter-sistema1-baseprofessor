namespace Univali.Api.Features.Publishers.Queries.GetPublishersDetail;

public class GetPublishersDetailDto {
    public int Id {get; set;}
    public string FirstName {get; set;} = string.Empty;
    public string LastName {get; set;} = string.Empty;
    public string Cpf {get; set;} = string.Empty;
}