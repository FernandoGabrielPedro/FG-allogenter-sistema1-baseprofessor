using AutoMapper;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler : IUpdateCustomerCommandHandler
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateCustomerCommand request, int idFromRoute)
    {
        Customer? customerEntity = await _customerRepository.GetCustomerByIdAsync(idFromRoute);
        if(customerEntity == null) return false;

        _mapper.Map(request, customerEntity);
        await _customerRepository.SaveChangesAsync();

        return true;
    }
}