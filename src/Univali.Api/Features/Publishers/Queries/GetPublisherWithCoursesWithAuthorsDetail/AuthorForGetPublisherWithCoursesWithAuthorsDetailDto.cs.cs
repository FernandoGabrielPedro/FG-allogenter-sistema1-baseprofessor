using Univali.Api.Entities;

namespace Univali.Api.Features.Publishers.Queries.GetPublisherWithCoursesWithAuthorsDetail;

public class AuthorForGetPublisherWithCoursesWithAuthorsDetailDto
{
    public int Id {get; set;}
    public string FirstName {get; set;} = string.Empty;
    public string LastName {get; set;} = string.Empty;
}