using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Commands.PatchCustomer;

public class PatchCustomerCommandHandler : IRequestHandler<PatchCustomerCommand, Customer?>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public PatchCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<Customer?> Handle(PatchCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer? customerFromDatabase = await _customerRepository.GetCustomerByIdAsync(request.Id);
        if (customerFromDatabase == null) return null;

        PatchCustomerDto customerToPatch = _mapper.Map<PatchCustomerDto>(customerFromDatabase);
        request.PatchDocument.ApplyTo(customerToPatch);

        Customer customerToReturn = _mapper.Map<Customer>(customerToPatch);
        return customerToReturn;
    }
}