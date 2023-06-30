namespace Univali.Api.Features.Publishers.Queries.GetPublisherWithCoursesDetail;

public class PublisherForGetPublisherWithCoursesDetailDto {
    public int Id {get; set;}
    public string FirstName {get; set;} = string.Empty;
    public string LastName {get; set;} = string.Empty;
    public string Cpf {get; set;} = string.Empty;
    public List<CourseForGetPublisherWithCoursesDetailDto> Courses {get; set;} = new();
}