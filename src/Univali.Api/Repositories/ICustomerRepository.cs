using Univali.Api.Entities;

namespace Univali.Api.Repositories;

public interface ICustomerRepository {
    Task<IEnumerable<Customer>> GetCustomersAsync();
    Customer? GetCustomerById(int id);
    Customer? GetCustomerByCpf(string cpf);
}