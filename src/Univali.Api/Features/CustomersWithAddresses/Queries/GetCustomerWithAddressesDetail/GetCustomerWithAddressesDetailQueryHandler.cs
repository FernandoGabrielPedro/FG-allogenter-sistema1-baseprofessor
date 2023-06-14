using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.CustomersWithAddresses.Queries.GetCustomerWithAddressesDetail;

public class GetCustomerWithAddressesDetailQueryHandler : IRequestHandler<GetCustomerWithAddressesDetailQuery, CustomerForGetCustomerWithAddressesDetailDto> {
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomerWithAddressesDetailQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<CustomerForGetCustomerWithAddressesDetailDto> Handle(GetCustomerWithAddressesDetailQuery request, CancellationToken cancellationToken)
    {
        Customer? customerWithAddressesFromDatabase = await _customerRepository.GetCustomerWithAddressesByIdAsync(request.Id);
        return _mapper.Map<CustomerForGetCustomerWithAddressesDetailDto>(customerWithAddressesFromDatabase);
    }
}