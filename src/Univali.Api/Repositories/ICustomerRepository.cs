using Univali.Api.Entities;
using Univali.Api.Models;

namespace Univali.Api.Repositories;

public interface ICustomerRepository {
    Task<bool> SaveChangesAsync();

    Task<IEnumerable<Customer>> GetCustomersAsync();
    Task<Customer?> GetCustomerByIdAsync(int id);
    Task<Customer?> GetCustomerByCpfAsync(string cpf);
    void CreateCustomer(Customer customerEntity);
    void UpdateCustomer(Customer customerEntity, Customer newCustomerValues);
    void DeleteCustomer(Customer customerEntity);
    
    Task<IEnumerable<Address>> GetAddressesAsync();
    Task<Address?> GetAddressByIdAsync(int id);
    Task<IEnumerable<Address>> GetAddressesByCustomerIdAsync(int customerId);
    void CreateAddress(Address addressEntity);
    void UpdateAddress(Address addressEntity, Address newAddressValues);
    void DeleteAddress(Address addressEntity);

    Task<IEnumerable<Customer>> GetCustomersWithAddressesAsync();
    Task<Customer?> GetCustomerWithAddressesByIdAsync(int id);
}