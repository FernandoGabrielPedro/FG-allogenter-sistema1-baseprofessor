namespace Univali.Api.Features.Customers.Queries.GetCustomerDetail;

public interface IGetCustomerDetailQueryHandler {
    Task<GetCustomerDetailDto?> Handle (GetCustomerDetailQuerie request);
}