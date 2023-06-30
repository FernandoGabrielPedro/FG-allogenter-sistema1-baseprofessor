using MediatR;

namespace Univali.Api.Features.Publishers.Queries.GetPublishersDetail;

public class GetPublishersDetailQuery : IRequest<IEnumerable<GetPublishersDetailDto>>{}