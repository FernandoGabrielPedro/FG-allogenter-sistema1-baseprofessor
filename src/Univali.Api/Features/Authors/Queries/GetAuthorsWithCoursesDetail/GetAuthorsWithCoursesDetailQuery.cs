using MediatR;

namespace Univali.Api.Features.Authors.Queries.GetAuthorsWithCoursesDetail;

public class GetAuthorsWithCoursesDetailQuery : IRequest<IEnumerable<AuthorForGetAuthorsWithCoursesDetailDto>> {}