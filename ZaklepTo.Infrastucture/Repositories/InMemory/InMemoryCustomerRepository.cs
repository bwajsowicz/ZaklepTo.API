using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZaklepTo.Core.Domain;
using ZaklepTo.Core.Repositories;

namespace ZaklepTo.Infrastructure.Repositories.InMemory
{
    public class InMemoryCustomerRepository : ICustomerRepository
    {
        private static readonly ISet<Customer> _customers = new HashSet<Customer>();

        public async Task AddAsync(Customer customer)
            => await Task.FromResult(_customers.Add(customer));

        public async Task DeleteAsync(string login)
        {
            var customer = await GetAsync(login);
            _customers.Remove(customer);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
            => await Task.FromResult(_customers);

        public async Task<Customer> GetAsync(string login)
            => await Task.FromResult(_customers.SingleOrDefault(x => x.Login == login));

        public async Task UpdateAsync(Customer customer)
        {
            var oldCustomer = await GetAsync(customer.Login);
            _customers.Remove(oldCustomer);
            _customers.Add(customer);
            await Task.CompletedTask;
        }
    }
}
