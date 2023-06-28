namespace Univali.Api.Features.Courses.Commands.CreateCourse;

public class CreateCourseDto{
    public int Id {get; set;}
    public string Title {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;
    public double Price {get; set;}
}