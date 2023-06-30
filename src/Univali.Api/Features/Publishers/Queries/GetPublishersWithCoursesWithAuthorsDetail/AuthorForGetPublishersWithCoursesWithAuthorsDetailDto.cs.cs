using Univali.Api.Entities;

namespace Univali.Api.Features.Publishers.Queries.GetPublishersWithCoursesWithAuthorsDetail;

public class AuthorForGetPublishersWithCoursesWithAuthorsDetailDto
{
    public int Id {get; set;}
    public string FirstName {get; set;} = string.Empty;
    public string LastName {get; set;} = string.Empty;
}