namespace Univali.Api.Features.Publishers.Queries.GetPublishersWithCoursesWithAuthorsDetail;

public class CourseForGetPublishersWithCoursesWithAuthorsDetailDto {
    public int Id {get; set;}
    public int PublisherId {get;set;}
    public string Title {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;
    public double Price {get; set;}
    public List<AuthorForGetPublishersWithCoursesWithAuthorsDetailDto> Authors {get; set;} = new();
}