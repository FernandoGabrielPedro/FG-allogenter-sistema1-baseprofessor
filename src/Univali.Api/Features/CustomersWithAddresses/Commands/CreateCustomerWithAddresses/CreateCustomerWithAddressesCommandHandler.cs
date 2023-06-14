using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.CustomersWithAddresses.Commands.CreateCustomerWithAddresses;

public class CreateCustomerWithAddressesCommandHandler : IRequestHandler<CreateCustomerWithAddressesCommand, CustomerToReturnForCreateCustomerWithAddressesDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CreateCustomerWithAddressesCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<CustomerToReturnForCreateCustomerWithAddressesDto> Handle(CreateCustomerWithAddressesCommand request, CancellationToken cancellationToken)
    {
        Customer customerEntity = _mapper.Map<Customer>(request);
        _customerRepository.CreateCustomer(customerEntity);
        await _customerRepository.SaveChangesAsync();
        CustomerToReturnForCreateCustomerWithAddressesDto customerToReturn = _mapper.Map<CustomerToReturnForCreateCustomerWithAddressesDto>(customerEntity);
        return customerToReturn;
    }
}