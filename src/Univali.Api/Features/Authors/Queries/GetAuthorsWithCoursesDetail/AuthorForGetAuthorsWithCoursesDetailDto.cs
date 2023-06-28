namespace Univali.Api.Features.Authors.Queries.GetAuthorsWithCoursesDetail;

public class AuthorForGetAuthorsWithCoursesDetailDto
{
    public int Id {get; set;}
    public string FirstName {get; set;} = string.Empty;
    public string LastName {get; set;} = string.Empty;
    public List<CourseForGetAuthorsWithCoursesDetailDto> Courses {get; set;} = new();
}