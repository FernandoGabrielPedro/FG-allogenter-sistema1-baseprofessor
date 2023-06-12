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

    public async Task<Customer?> GetCustomerByIdAsync(int id) {
        IEnumerable<Customer> customersList = await _context.Customers.ToListAsync();
        return customersList.FirstOrDefault(c => c.Id == id);
    }

    public async Task<Customer?> GetCustomerByCpfAsync(string cpf) {
        IEnumerable<Customer> customersList = await _context.Customers.ToListAsync();
        return customersList.FirstOrDefault(c => c.Cpf == cpf);
    }

    public async void CreateCustomerAsync(Customer customerEntity) {
        customerEntity.Id = _context.Customers.Max(c => c.Id) + 1;
        await _context.Customers.AddAsync(customerEntity);
        await _context.SaveChangesAsync();
    }

    public async void UpdateCustomerAsync(Customer customerFromDatabase, CustomerForUpdateDto customerForUpdateDto) {
        _mapper.Map(customerForUpdateDto, customerFromDatabase);
        await _context.SaveChangesAsync();
    }

    public async void DeleteCustomerAsync(Customer customerFromDatabase) {
        _context.Customers.Remove(customerFromDatabase);
        await _context.SaveChangesAsync();
    }

    public async void PartiallyUpdateCustomerAsync()
    {
        throw new NotImplementedException();
    }



    public async Task<IEnumerable<Address>> GetAddressesAsync() {
        return await _context.Addresses.OrderBy(c => c.Id).ToListAsync();
    }

    public async Task<Address?> GetAddressByIdAsync(int id) {
        IEnumerable<Address> addressList = await _context.Addresses.ToListAsync();
        return addressList.FirstOrDefault(c => c.Id == id);
    }

    public async Task<IEnumerable<Address>> GetAddressesByCustomerIdAsync(int customerId) {
        IEnumerable<Address> addressList = await _context.Addresses.ToListAsync();
        return addressList.ToList().FindAll(a => a.CustomerId == customerId).OrderBy(c => c.Id);
    }

    public async void CreateAddressAsync(Address addressEntity) {
        addressEntity.Id = _context.Addresses.Max(c => c.Id) + 1;
        await _context.Addresses.AddAsync(addressEntity);
        await _context.SaveChangesAsync();
    }
    public async void UpdateAddressAsync(Address addressFromDatabase, AddressForUpdateDto addressForUpdateDto) {
        _mapper.Map(addressForUpdateDto, addressFromDatabase);
        await _context.SaveChangesAsync();
    }
    public async void DeleteAddressAsync(Address addressFromDatabase) {
        _context.Addresses.Remove(addressFromDatabase);
        await _context.SaveChangesAsync();
    }
}