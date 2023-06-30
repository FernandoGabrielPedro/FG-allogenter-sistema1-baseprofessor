namespace Univali.Api.Features.Publishers.Commands.CreatePublisher;

public class CreatePublisherDto
{
    public int Id {get; set;}
    public string FirstName {get; set;} = string.Empty;
    public string LastName {get; set;} = string.Empty;
    public string Cpf {get; set;} = string.Empty;
    // public List<Course> Courses {get; set;} = new();         Sem Course
}