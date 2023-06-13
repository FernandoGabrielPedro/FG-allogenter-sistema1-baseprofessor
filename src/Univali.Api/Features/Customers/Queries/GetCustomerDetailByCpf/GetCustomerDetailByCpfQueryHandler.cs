using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Customers.Queries.GetCustomerDetailByCpf;

public class GetCustomerDetailByCpfQueryHandler : IRequestHandler<GetCustomerDetailByCpfQuery, GetCustomerDetailByCpfDto> {
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomerDetailByCpfQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<GetCustomerDetailByCpfDto> Handle(GetCustomerDetailByCpfQuery request, CancellationToken cancellationToken)
    {
        Customer? customerFromDatabase = await _customerRepository.GetCustomerByCpfAsync(request.Cpf);
        return _mapper.Map<GetCustomerDetailByCpfDto>(customerFromDatabase);
    }
}