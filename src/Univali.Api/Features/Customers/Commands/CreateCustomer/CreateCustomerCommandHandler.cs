using AutoMapper;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandler : ICreateCustomerCommandHandler
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<CreateCustomerDto> Handle(CreateCustomerCommand request)
    {
        Customer customerEntity = _mapper.Map<Customer>(request);
        _customerRepository.CreateCustomer(customerEntity);
        await _customerRepository.SaveChangesAsync();
        CreateCustomerDto customerToReturn = _mapper.Map<CreateCustomerDto>(customerEntity);
        return customerToReturn;
    }
}