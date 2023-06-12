using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Univali.Api.DbContexts;
using Univali.Api.Entities;
using Univali.Api.Models;

namespace Univali.Api.Repositories;

public class CustomerRepository : ICustomerRepository {
    private readonly CustomerContext _context;
    private readonly IMapper _mapper;

    public CustomerRepository(CustomerContext customerContext, IMapper mapper) {
        _context = customerContext ?? throw new ArgumentNullException(nameof(customerContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<Customer>> GetCustomersAsync() {
        return await _context.Customers.OrderBy(c => c.Id).ToListAsync();
    }

    public Customer? GetCustomerById(int id) {
        return _context.Customers.FirstOrDefault(c => c.Id == id);
    }

    public Customer? GetCustomerByCpf(string cpf) {
        return _context.Customers.FirstOrDefault(c => c.Cpf == cpf);
    }

    public void CreateCustomer(Customer customerEntity) {
        customerEntity.Id = _context.Customers.Max(c => c.Id) + 1;
        _context.Customers.Add(customerEntity);
        _context.SaveChanges();
    }

    public void UpdateCustomer(Customer customerFromDatabase, CustomerForUpdateDto customerForUpdateDto) {
        _mapper.Map(customerForUpdateDto, customerFromDatabase);
        _context.SaveChanges();
    }

    public void DeleteCustomer(Customer customerFromDatabase) {
        _context.Customers.Remove(customerFromDatabase);
        _context.SaveChanges();
    }

    public void PartiallyUpdateCustomer()
    {
        throw new NotImplementedException();
    }



    public async Task<IEnumerable<Address>> GetAddressesAsync() {
        return await _context.Addresses.OrderBy(c => c.Id).ToListAsync();
    }

    public Address? GetAddressById(int id) {
        return _context.Addresses.FirstOrDefault(c => c.Id == id);
    }

    public async Task<IEnumerable<Address>> GetAddressesByCustomerIdAsync(int customerId) {
        //return await _context.Addresses.Where(a => a.CustomerId == customerId).OrderBy(c => c.Id).ToListAsync();
        IEnumerable<Address> addressList = await _context.Addresses.ToListAsync();
        return addressList.ToList().FindAll(a => a.CustomerId == customerId).OrderBy(c => c.Id);
    }

    public void CreateAddress(Address addressEntity) {
        addressEntity.Id = _context.Addresses.Max(c => c.Id) + 1;
        _context.Addresses.Add(addressEntity);
        _context.SaveChanges();
    }
    public void UpdateAddress(Address addressFromDatabase, AddressForUpdateDto addressForUpdateDto) {
        _mapper.Map(addressForUpdateDto, addressFromDatabase);
        _context.SaveChanges();
    }
    public void DeleteAddress(Address addressFromDatabase) {
        _context.Addresses.Remove(addressFromDatabase);
        _context.SaveChanges();
    }
}