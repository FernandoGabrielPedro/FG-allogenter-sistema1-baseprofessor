using AutoMapper;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Queries.GetCustomersDetail;

public class GetCustomersDetailQuerieHandler : IGetCustomersDetailQueryHandler {
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomersDetailQuerieHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetCustomersDetailDto?>> Handle (GetCustomersDetailQuerie request) {
        IEnumerable<Customer?> customersFromDatabase = await _customerRepository.GetCustomersAsync();
        return _mapper.Map<IEnumerable<GetCustomersDetailDto>>(customersFromDatabase);
    }
}