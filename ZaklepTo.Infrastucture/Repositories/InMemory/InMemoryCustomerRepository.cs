using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZaklepTo.Core.Domain;
using ZaklepTo.Core.Repositories;

namespace ZaklepTo.Infrastructure.Repositories.InMemory
{
    public class InMemoryCustomerRepository : ICustomerRepository
    {
        private static readonly ISet<Customer> Customers = new HashSet<Customer>();

        public async Task AddAsync(Customer customer)
            => await Task.FromResult(Customers.Add(customer));

        public async Task DeleteAsync(string login)
        {
            var customer = await GetAsync(login);
            Customers.Remove(customer);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
            => await Task.FromResult(Customers);

        public async Task<Customer> GetAsync(string login)
            => await Task.FromResult(Customers.SingleOrDefault(x => x.Login == login));

        public async Task UpdateAsync(Customer customer)
        {
            var oldCustomer = await GetAsync(customer.Login);
            Customers.Remove(oldCustomer);
            Customers.Add(customer);
            await Task.CompletedTask;
        }
    }
}
