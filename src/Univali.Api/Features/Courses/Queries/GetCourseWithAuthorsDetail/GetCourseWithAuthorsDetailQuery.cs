using MediatR;

namespace Univali.Api.Features.Courses.Queries.GetCourseWithAuthorsDetail;

public class GetCourseWithAuthorsDetailQuery : IRequest<CourseForGetCourseWithAuthorsDetailDto> {
    public int CourseId {get; set;}
    public int PublisherId {get; set;}
}