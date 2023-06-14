using MediatR;

namespace Univali.Api.Features.CustomersWithAddresses.Queries.GetCustomersWithAddressesDetail;

public class GetCustomersWithAddressesDetailQuery : IRequest<IEnumerable<CustomerForGetCustomersWithAddressesDetailDto>> { }