namespace Univali.Api.Features.Courses.Queries.GetCoursesWithAuthorsDetail;

public class AuthorForGetCoursesWithAuthorsDetailDto
{
    public int Id {get; set;}
    public string FirstName {get; set;} = string.Empty;
    public string LastName {get; set;} = string.Empty;
}