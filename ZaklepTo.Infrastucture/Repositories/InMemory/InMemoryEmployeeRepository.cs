using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZaklepTo.Core.Domain;
using ZaklepTo.Core.Repositories;

namespace ZaklepTo.Infrastructure.Repositories.InMemory
{
    public class InMemoryEmployeeRepository : IEmployeeRepository
    {
        private static readonly ISet<Employee> Employees = new HashSet<Employee>();

        public async Task AddAsync(Employee employee)
            => await Task.FromResult(Employees.Add(employee));

        public async Task DeleteAsync(string login)
        {
            var employee = await GetAsync(login);
            Employees.Remove(employee);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
            => await Task.FromResult(Employees);

        public async Task<Employee> GetAsync(string login)
            => await Task.FromResult(Employees.SingleOrDefault(x => x.Login == login));

        public async Task UpdateAsync(Employee employee)
        {
            var oldEmployee = await GetAsync(employee.Login);
            Employees.Remove(oldEmployee);
            Employees.Add(employee);
            await Task.CompletedTask;
        }
    }
}
