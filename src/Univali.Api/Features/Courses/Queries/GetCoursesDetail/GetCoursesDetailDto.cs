using Univali.Api.Entities;

namespace Univali.Api.Features.Courses.Queries.GetCoursesDetail;

public class GetCoursesDetailDto
{
    public int Id {get; set;}
    public string Title {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;
    public double Price {get; set;}
}