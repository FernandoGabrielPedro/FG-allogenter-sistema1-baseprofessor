using MediatR;

namespace Univali.Api.Features.Courses.Queries.GetCoursesWithAuthorsDetail;

public class GetCoursesWithAuthorsDetailQuery : IRequest<IEnumerable<CourseForGetCoursesWithAuthorsDetailDto>> {
    public int PublisherId {get; set;}
}