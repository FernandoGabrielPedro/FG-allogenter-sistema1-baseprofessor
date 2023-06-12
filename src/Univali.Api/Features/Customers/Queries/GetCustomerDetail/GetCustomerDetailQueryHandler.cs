using AutoMapper;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Queries.GetCustomerDetail;

public class GetCustomerDetailQuerieHandler : IGetCustomerDetailQueryHandler {
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomerDetailQuerieHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<GetCustomerDetailDto?> Handle (GetCustomerDetailQuerie request) {
        Customer? customerFromDatabase = await _customerRepository.GetCustomerByIdAsync(request.Id);
        return _mapper.Map<GetCustomerDetailDto>(customerFromDatabase);
    }
}