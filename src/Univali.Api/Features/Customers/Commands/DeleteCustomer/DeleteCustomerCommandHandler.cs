using AutoMapper;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommandHandler : IDeleteCustomerCommandHandler
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public DeleteCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteCustomerCommand request)
    {
        Customer? customerEntity = await _customerRepository.GetCustomerByIdAsync(request.Id);
        if(customerEntity == null) return false;

        _customerRepository.DeleteCustomer(customerEntity);
        await _customerRepository.SaveChangesAsync();

        return true;
    }
}