using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Addresses.Queries.GetAddressesDetail;

public class GetAddressesDetailQueryHandler : IRequestHandler<GetAddressesDetailQuery, IEnumerable<GetAddressesDetailDto>> {
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetAddressesDetailQueryHandler(ICustomerRepository CustomerRepository, IMapper mapper)
    {
        _customerRepository = CustomerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetAddressesDetailDto>> Handle(GetAddressesDetailQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Address?> AddressesFromDatabase = await _customerRepository.GetAddressesAsync();
        return _mapper.Map<IEnumerable<GetAddressesDetailDto>>(AddressesFromDatabase);
    }
}