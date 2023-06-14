using MediatR;

namespace Univali.Api.Features.Addresses.Queries.GetAddressesDetailByCustomerId;

public class GetAddressesDetailByCustomerIdQuery : IRequest<IEnumerable<GetAddressesDetailByCustomerIdDto>> {
    public int CustomerId {get; set;}
}