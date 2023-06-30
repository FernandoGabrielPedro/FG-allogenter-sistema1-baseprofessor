namespace Univali.Api.Features.Publishers.Queries.GetPublisherDetail;

public class GetPublisherDetailDto {
    public int Id {get; set;}
    public string FirstName {get; set;} = string.Empty;
    public string LastName {get; set;} = string.Empty;
    public string Cpf {get; set;} = string.Empty;
}