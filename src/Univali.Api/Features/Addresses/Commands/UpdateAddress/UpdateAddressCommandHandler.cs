using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Addresses.Commands.UpdateAddress;

public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, bool>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public UpdateAddressCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        Address? addressEntity = await _customerRepository.GetAddressByIdAsync(request.Id);
        if(addressEntity == null) return false;

        _mapper.Map(request, addressEntity);
        await _customerRepository.SaveChangesAsync();

        return true;
    }
}