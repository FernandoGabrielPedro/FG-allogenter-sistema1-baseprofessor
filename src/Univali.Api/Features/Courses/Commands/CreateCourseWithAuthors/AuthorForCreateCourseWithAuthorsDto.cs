namespace Univali.Api.Features.Courses.Commands.CreateCourseWithAuthors;

public class AuthorForCreateCourseWithAuthorsDto
{
    public int Id {get; set;}
    public string FirstName {get; set;} = string.Empty;
    public string LastName {get; set;} = string.Empty;
}