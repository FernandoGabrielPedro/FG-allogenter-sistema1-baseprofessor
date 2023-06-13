using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Queries.GetCustomersDetail;

public class GetCustomersDetailQueryHandler : IRequestHandler<GetCustomersDetailQuery, IEnumerable<GetCustomersDetailDto>> {
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomersDetailQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetCustomersDetailDto>> Handle(GetCustomersDetailQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Customer?> customersFromDatabase = await _customerRepository.GetCustomersAsync();
        return _mapper.Map<IEnumerable<GetCustomersDetailDto>>(customersFromDatabase);
    }
}