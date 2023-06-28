namespace Univali.Api.Features.Courses.Commands.CreateCourseWithAuthors;

public class CourseForCreateCourseWithAuthorsDto{
    public int Id {get; set;}
    public string Title {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;
    public double Price {get; set;}
    public List<AuthorForCreateCourseWithAuthorsDto> Authors {get; set;} = new();
}