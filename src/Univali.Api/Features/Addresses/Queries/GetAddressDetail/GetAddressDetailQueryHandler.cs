using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Addresses.Queries.GetAddressDetail;

public class GetAddressDetailQueryHandler : IRequestHandler<GetAddressDetailQuery, GetAddressDetailDto> {
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetAddressDetailQueryHandler(ICustomerRepository CustomerRepository, IMapper mapper)
    {
        _customerRepository = CustomerRepository;
        _mapper = mapper;
    }

    public async Task<GetAddressDetailDto> Handle(GetAddressDetailQuery request, CancellationToken cancellationToken)
    {
        Address? AddressFromDatabase = await _customerRepository.GetAddressByIdAsync(request.Id);
        return _mapper.Map<GetAddressDetailDto>(AddressFromDatabase);
    }
}