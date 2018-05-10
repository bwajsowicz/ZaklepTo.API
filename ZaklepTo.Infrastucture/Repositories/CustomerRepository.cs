using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZaklepTo.Core.Domain;
using ZaklepTo.Core.Repositories;
using ZaklepTo.Infrastructure.EntityFramwerork;

namespace ZaklepTo.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ZaklepToContext _context;

        public CustomerRepository(ZaklepToContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string login)
        {
            var customerToRemove = await _context.Customers.FindAsync(login);
           _context.Customers.Remove(customerToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
            => await _context.Customers.ToListAsync();

        public async Task<Customer> GetAsync(string login)
            => await _context.Customers.FindAsync(login);

        public async Task UpdateAsync(Customer customer)
        {
            _context.Update(customer);
            await _context.SaveChangesAsync();
        }
    }
}
