using MediatR;

namespace Univali.Api.Features.Customers.Queries.GetCustomersDetail;

public class GetCustomersDetailQuery : IRequest<IEnumerable<GetCustomersDetailDto>> { }