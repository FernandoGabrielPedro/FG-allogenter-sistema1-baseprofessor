namespace Univali.Api.Features.Publishers.Queries.GetPublishersWithCoursesWithAuthorsDetail;

public class PublisherForGetPublishersWithCoursesWithAuthorsDetailDto
{
    public int Id {get; set;}
    public string FirstName {get; set;} = string.Empty;
    public string LastName {get; set;} = string.Empty;
    public string Cpf {get; set;} = string.Empty;
    public List<CourseForGetPublishersWithCoursesWithAuthorsDetailDto> Courses {get; set;} = new();
}