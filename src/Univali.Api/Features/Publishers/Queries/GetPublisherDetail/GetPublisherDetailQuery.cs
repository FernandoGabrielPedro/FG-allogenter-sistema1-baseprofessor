using MediatR;

namespace Univali.Api.Features.Publishers.Queries.GetPublisherDetail;

public class GetPublisherDetailQuery : IRequest<GetPublisherDetailDto> {
    public int Id {get; set;}
}