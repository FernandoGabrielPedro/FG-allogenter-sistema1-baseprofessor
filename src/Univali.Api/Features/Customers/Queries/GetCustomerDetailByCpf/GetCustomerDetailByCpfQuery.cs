using MediatR;

namespace Univali.Api.Features.Customers.Queries.GetCustomerDetailByCpf;

public class GetCustomerDetailByCpfQuery : IRequest<GetCustomerDetailByCpfDto> {
    public string Cpf {get; set;} = String.Empty;
}