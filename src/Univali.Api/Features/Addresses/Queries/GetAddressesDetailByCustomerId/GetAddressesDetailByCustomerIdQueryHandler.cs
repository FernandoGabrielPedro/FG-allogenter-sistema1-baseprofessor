using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Addresses.Queries.GetAddressesDetailByCustomerId;

public class GetAddressesDetailByCustomerIdQueryHandler : IRequestHandler<GetAddressesDetailByCustomerIdQuery, IEnumerable<GetAddressesDetailByCustomerIdDto>> {
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetAddressesDetailByCustomerIdQueryHandler(ICustomerRepository CustomerRepository, IMapper mapper)
    {
        _customerRepository = CustomerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetAddressesDetailByCustomerIdDto>> Handle(GetAddressesDetailByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Address?> AddressesFromDatabase = await _customerRepository.GetAddressesByCustomerIdAsync(request.CustomerId);
        return _mapper.Map<IEnumerable<GetAddressesDetailByCustomerIdDto>>(AddressesFromDatabase);
    }
}