using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.CustomersWithAddresses.Commands.UpdateCustomerWithAddresses;

public class UpdateCustomerWithAddressesCommandHandler : IRequestHandler<UpdateCustomerWithAddressesCommand, bool>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public UpdateCustomerWithAddressesCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateCustomerWithAddressesCommand request, CancellationToken cancellationToken)
    {
        Customer? customerFromDatabase = await _customerRepository.GetCustomerWithAddressesByIdAsync(request.Id);
        if (customerFromDatabase == null) return false;

        Customer updatedCustomer = _mapper.Map<Customer>(request);
        _mapper.Map(updatedCustomer, customerFromDatabase);
        await _customerRepository.SaveChangesAsync();

        return true;
    }
}