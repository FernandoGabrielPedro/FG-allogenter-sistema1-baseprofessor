using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Addresses.Commands.DeleteAddress;

public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, bool>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public DeleteAddressCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        Address? addressEntity = await _customerRepository.GetAddressByIdAsync(request.Id);
        if(addressEntity == null) return false;

        _customerRepository.DeleteAddress(addressEntity);
        await _customerRepository.SaveChangesAsync();

        return true;
    }
}