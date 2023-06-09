using Microsoft.EntityFrameworkCore;
using Univali.Api.DbContexts;
using Univali.Api.Entities;

namespace Univali.Api.Repositories;

public class CustomerRepository : ICustomerRepository {
    private readonly CustomerContext _context;

    public CustomerRepository(CustomerContext customerContext) {
        _context = customerContext;
    }

    public async Task<IEnumerable<Customer>> GetCustomersAsync()
    {
        return await _context.Customers.OrderBy(c => c.Id).ToListAsync();
    }

    public Customer? GetCustomerById(int id)
    {
        return _context.Customers.FirstOrDefault(c => c.Id == id);
    }

    public Customer? GetCustomerByCpf(string cpf)
    {
        return _context.Customers.FirstOrDefault(c => c.Cpf == cpf);
    }
}