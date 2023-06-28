namespace Univali.Api.Features.Courses.Queries.GetCoursesWithAuthorsDetail;

public class CourseForGetCoursesWithAuthorsDetailDto {
    public int Id {get; set;}
    public string Title {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;
    public double Price {get; set;}
    public List<AuthorForGetCoursesWithAuthorsDetailDto> Authors {get; set;} = new();
}