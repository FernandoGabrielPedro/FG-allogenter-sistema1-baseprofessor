using MediatR;

namespace Univali.Api.Features.CustomersWithAddresses.Queries.GetCustomerWithAddressesDetail;

public class GetCustomerWithAddressesDetailQuery : IRequest<CustomerForGetCustomerWithAddressesDetailDto> {
    public int Id {get; set;}
}