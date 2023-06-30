namespace Univali.Api.Features.Publishers.Queries.GetPublisherWithCoursesWithAuthorsDetail;

public class PublisherForGetPublisherWithCoursesWithAuthorsDetailDto {
    public int Id {get; set;}
    public string FirstName {get; set;} = string.Empty;
    public string LastName {get; set;} = string.Empty;
    public string Cpf {get; set;} = string.Empty;
    public List<CourseForGetPublisherWithCoursesWithAuthorsDetailDto> Courses {get; set;} = new();
}