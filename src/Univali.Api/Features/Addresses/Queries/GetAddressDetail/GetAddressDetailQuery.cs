using MediatR;

namespace Univali.Api.Features.Addresses.Queries.GetAddressDetail;

public class GetAddressDetailQuery : IRequest<GetAddressDetailDto> {
    public int Id {get; set;}
}