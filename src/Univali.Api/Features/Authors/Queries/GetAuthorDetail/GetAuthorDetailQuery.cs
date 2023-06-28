using MediatR;

namespace Univali.Api.Features.Authors.Queries.GetAuthorDetail;

public class GetAuthorDetailQuery : IRequest<GetAuthorDetailDto> {
    public int Id {get; set;}
}