using AutoMapper;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Queries.GetCustomerDetailByCpf;

public class GetCustomerDetailByCpfQuerieHandler : IGetCustomerDetailByCpfQueryHandler {
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomerDetailByCpfQuerieHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<GetCustomerDetailByCpfDto?> Handle (GetCustomerDetailByCpfQuerie request) {
        Customer? customerFromDatabase = await _customerRepository.GetCustomerByCpfAsync(request.Cpf);
        return _mapper.Map<GetCustomerDetailByCpfDto>(customerFromDatabase);
    }
}