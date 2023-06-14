using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Addresses.Commands.CreateAddress;

public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, CreateAddressDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CreateAddressCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<CreateAddressDto> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        Address addressEntity = _mapper.Map<Address>(request);
        _customerRepository.CreateAddress(addressEntity);
        await _customerRepository.SaveChangesAsync();
        CreateAddressDto addressToReturn = _mapper.Map<CreateAddressDto>(addressEntity);
        return addressToReturn;
    }
}