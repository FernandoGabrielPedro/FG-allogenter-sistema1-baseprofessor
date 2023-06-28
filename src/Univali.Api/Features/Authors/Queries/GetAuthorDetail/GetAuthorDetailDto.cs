using Univali.Api.Entities;

namespace Univali.Api.Features.Authors.Queries.GetAuthorDetail;

public class GetAuthorDetailDto
{
    public int Id {get; set;}
    public string FirstName {get; set;} = string.Empty;
    public string LastName {get; set;} = string.Empty;
}