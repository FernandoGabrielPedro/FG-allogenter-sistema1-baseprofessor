using Univali.Api.Entities;
using Univali.Api.Models;

namespace Univali.Api.Repositories;

public interface ICustomerRepository {
    Task<IEnumerable<Customer>> GetCustomersAsync();
    Customer? GetCustomerById(int id);
    Customer? GetCustomerByCpf(string cpf);
    void CreateCustomer(Customer customerEntity);
    void UpdateCustomer(Customer customerFromDatabase, CustomerForUpdateDto customerForUpdateDto);
    void DeleteCustomer(Customer customerFromDatabase);
    void PartiallyUpdateCustomer();
    
    Task<IEnumerable<Address>> GetAddressesAsync();
    Address? GetAddressById(int id);
    Task<IEnumerable<Address>> GetAddressesByCustomerIdAsync(int customerId);
    void CreateAddress(Address addressEntity);
    void UpdateAddress(Address addressFromDatabase, AddressForUpdateDto addressForUpdateDto);
    void DeleteAddress(Address addressFromDatabase);
}