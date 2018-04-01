using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZaklepTo.Core.Domain;
using ZaklepTo.Core.Repositories;

namespace ZaklepTo.Infrastructure.Repositories.InMemory
{
    public class InMemoryCustomerRepository : ICustomerRepository
    {
        private static readonly ISet<Customer> _customers = new HashSet<Customer>
        {
            new Customer("mkowalski", "Madam", "Kowalski", "kkowalski@example.com", "123-123-123", "pass1", "salt"),
            new Customer("mbodzion", "Maciej", "Bodzion", "mbodzion@example.com", "213-543-185", "pass2", "salt"),
            new Customer("kprymusowicz", "Kamil", "Prymusowicz", "bprymusowicz@example.com", "123-123-555", "pass3", "salt")
        };

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
