using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Univali.Api.DbContexts;
using Univali.Api.Entities;
using Univali.Api.Models;

namespace Univali.Api.Repositories;

public class CustomerRepository : ICustomerRepository {
    private readonly CustomerContext _context;

    public CustomerRepository(CustomerContext customerContext) {
        _context = customerContext ?? throw new ArgumentNullException(nameof(customerContext));
    }



    public async Task<bool> SaveChangesAsync() {
        return (await _context.SaveChangesAsync() > 0);
    }



    public async Task<IEnumerable<Customer>> GetCustomersAsync() {
        return await _context.Customers.OrderBy(c => c.Id).ToListAsync();
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id) {
        IEnumerable<Customer> customersList = await _context.Customers.ToListAsync();
        return customersList.FirstOrDefault(c => c.Id == id);
    }

    public async Task<Customer?> GetCustomerByCpfAsync(string cpf) {
        IEnumerable<Customer> customersList = await _context.Customers.ToListAsync();
        return customersList.FirstOrDefault(c => c.Cpf == cpf);
    }

    public void CreateCustomer(Customer customerEntity) {
        _context.Customers.Add(customerEntity);
    }

    public void DeleteCustomer(Customer customerEntity) {
        _context.Customers.Remove(customerEntity);
    }

    public async void PartiallyUpdateCustomerAsync()
    {
        throw new NotImplementedException();
    }



    public async Task<IEnumerable<Address>> GetAddressesAsync() {
        return await _context.Addresses.OrderBy(a => a.Id).ToListAsync();
    }

    public async Task<Address?> GetAddressByIdAsync(int id) {
        IEnumerable<Address> addressList = await _context.Addresses.ToListAsync();
        return addressList.FirstOrDefault(a => a.Id == id);
    }

    public async Task<IEnumerable<Address>> GetAddressesByCustomerIdAsync(int customerId) {
        IEnumerable<Address> addressList = await _context.Addresses.ToListAsync();
        return addressList.ToList().FindAll(a => a.CustomerId == customerId).OrderBy(c => c.Id);
    }

    public void CreateAddress(Address addressEntity) {
        _context.Addresses.AddAsync(addressEntity);
    }
    public void DeleteAddress(Address addressEntity) {
        _context.Addresses.Remove(addressEntity);
    }



    public async Task<IEnumerable<Customer>> GetCustomersWithAddressesAsync() {
        return await _context.Customers.Include(c => c.Addresses).OrderBy(c => c.Id).ToListAsync();
    }

    public async Task<Customer?> GetCustomerWithAddressesByIdAsync(int id) {
        IEnumerable<Customer> customersList = await _context.Customers.Include(c => c.Addresses).ToListAsync();
        return customersList.FirstOrDefault(c => c.Id == id);
    }
}