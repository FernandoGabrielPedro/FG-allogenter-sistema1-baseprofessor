using MediatR;

namespace Univali.Api.Features.Addresses.Queries.GetAddressesDetail;

public class GetAddressesDetailQuery : IRequest<IEnumerable<GetAddressesDetailDto>> { }