using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using ZaklepTo.Core.Domain;
using ZaklepTo.Core.Repositories;

namespace ZaklepTo.Infrastucture.Repositories.InMemory
{
    class EmployeeRepository : IEmployeeRepository
    {
        //nulls
        private ISet<Employee> _employees = new HashSet<Employee>
        {
            new Employee("prac1", "Jan", "Kowalski", "jkowalski@example.com", "123-123-123", "pass1", "salt", null),
            new Employee("prac2", "Zbigniew", "Phrymus", "jphrymus@example.com", "321-321-321", "pass2", "salt", null),
            new Employee("prac3", "Zbigniew", "Huston", "jhuston@example.com", "321-555-321", "pass3", "salt", null)

        };

        public async Task AddAsync(Employee employee)
            => await Task.FromResult(_employees.Add(employee));

        public async Task DeleteAsync(string email)
        {
            var employee = await GetAsync(email);
            _employees.Remove(employee);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
            => await Task.FromResult(_employees);

        public async Task<Employee> GetAsync(string email)
            => await Task.FromResult(_employees.SingleOrDefault(x => x.Email == email));

        public async Task UpdateAsync(Employee employee)
        {
            var oldEmployee = await GetAsync(employee.Email);
            _employees.Remove(oldEmployee);
            _employees.Add(employee);
            await Task.CompletedTask;
        }
    }
}
