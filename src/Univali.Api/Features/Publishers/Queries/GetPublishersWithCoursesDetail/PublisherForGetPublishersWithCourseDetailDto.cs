namespace Univali.Api.Features.Publishers.Queries.GetPublishersWithCoursesDetail;

public class PublisherForGetPublishersWithCoursesDetailDto
{
    public int Id {get; set;}
    public string FirstName {get; set;} = string.Empty;
    public string LastName {get; set;} = string.Empty;
    public string Cpf {get; set;} = string.Empty;
    public List<CourseForGetPublishersWithCoursesDetailDto> Courses {get; set;} = new();
}