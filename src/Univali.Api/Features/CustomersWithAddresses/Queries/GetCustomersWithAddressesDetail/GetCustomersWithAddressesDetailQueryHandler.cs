using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.CustomersWithAddresses.Queries.GetCustomersWithAddressesDetail;

public class GetCustomersWithAddressesDetailQueryHandler : IRequestHandler<GetCustomersWithAddressesDetailQuery, IEnumerable<CustomerForGetCustomersWithAddressesDetailDto>> {
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomersWithAddressesDetailQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerForGetCustomersWithAddressesDetailDto>> Handle(GetCustomersWithAddressesDetailQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Customer?> customerswithaddressesFromDatabase = await _customerRepository.GetCustomersWithAddressesAsync();
        return _mapper.Map<IEnumerable<CustomerForGetCustomersWithAddressesDetailDto>>(customerswithaddressesFromDatabase);
    }
}