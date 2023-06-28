using MediatR;

namespace Univali.Api.Features.Courses.Queries.GetCoursesDetail;

public class GetCoursesDetailQuery : IRequest<IEnumerable<GetCoursesDetailDto>> {}