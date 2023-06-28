namespace Univali.Api.Features.Authors.Queries.GetAuthorWithCoursesDetail;

public class AuthorForGetAuthorWithCoursesDetailDto
{
    public int Id {get; set;}
    public string FirstName {get; set;} = string.Empty;
    public string LastName {get; set;} = string.Empty;
    public List<CourseForGetAuthorWithCoursesDetailDto> Courses {get; set;} = new();
}