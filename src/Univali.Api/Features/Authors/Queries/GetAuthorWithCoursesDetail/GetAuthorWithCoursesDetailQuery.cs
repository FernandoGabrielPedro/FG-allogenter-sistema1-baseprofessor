using MediatR;

namespace Univali.Api.Features.Authors.Queries.GetAuthorWithCoursesDetail;

public class GetAuthorWithCoursesDetailQuery : IRequest<AuthorForGetAuthorWithCoursesDetailDto> {
    public int Id {get; set;}
}