using Univali.Api.Entities;
using Univali.Api.Models;

namespace Univali.Api.Repositories;

public interface ICustomerRepository {
    Task<IEnumerable<Customer>> GetCustomersAsync();
    Task<Customer?> GetCustomerByIdAsync(int id);
    Task<Customer?> GetCustomerByCpfAsync(string cpf);
    void CreateCustomerAsync(Customer customerEntity);
    void UpdateCustomerAsync(Customer customerFromDatabase, CustomerForUpdateDto customerForUpdateDto);
    void DeleteCustomerAsync(Customer customerFromDatabase);
    void PartiallyUpdateCustomerAsync();
    
    Task<IEnumerable<Address>> GetAddressesAsync();
    Task<Address?> GetAddressByIdAsync(int id);
    Task<IEnumerable<Address>> GetAddressesByCustomerIdAsync(int customerId);
    void CreateAddressAsync(Address addressEntity);
    void UpdateAddressAsync(Address addressFromDatabase, AddressForUpdateDto addressForUpdateDto);
    void DeleteAddressAsync(Address addressFromDatabase);
}