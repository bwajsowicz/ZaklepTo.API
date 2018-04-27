using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZaklepTo.Core.Domain;
using ZaklepTo.Core.Repositories;
using ZaklepTo.Infrastructure.Services.Implementations;

namespace ZaklepTo.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataBaseService _dataBaseService;

        public CustomerRepository(DataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public async Task AddAsync(Customer customer)
        {
            await _dataBaseService.Customers.AddAsync(customer);
            await _dataBaseService.SaveChangesAsync();
        }

        public async Task DeleteAsync(string login)
        {
            var customerToRemove = await _dataBaseService.Customers.FindAsync(login);
            _dataBaseService.Customers.Remove(customerToRemove);
            await _dataBaseService.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
            => await _dataBaseService.Customers.ToListAsync();

        public async Task<Customer> GetAsync(string login)
            => await _dataBaseService.Customers.FindAsync(login);

        public async Task UpdateAsync(Customer customer)
        {
            _dataBaseService.Update(customer);
            await _dataBaseService.SaveChangesAsync();
        }
    }
}
