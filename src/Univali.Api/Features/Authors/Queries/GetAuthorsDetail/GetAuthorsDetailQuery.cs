using MediatR;

namespace Univali.Api.Features.Authors.Queries.GetAuthorsDetail;

public class GetAuthorsDetailQuery : IRequest<IEnumerable<GetAuthorsDetailDto>> {}