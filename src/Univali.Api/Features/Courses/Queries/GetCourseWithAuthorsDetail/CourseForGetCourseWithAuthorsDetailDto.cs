namespace Univali.Api.Features.Courses.Queries.GetCourseWithAuthorsDetail;

public class CourseForGetCourseWithAuthorsDetailDto {
    public int Id {get; set;}
    public string Title {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;
    public double Price {get; set;}
    public List<AuthorForGetCourseWithAuthorsDetailDto> Authors {get; set;} = new();
}