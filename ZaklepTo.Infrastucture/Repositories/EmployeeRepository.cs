using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZaklepTo.Core.Domain;
using ZaklepTo.Core.Repositories;
using ZaklepTo.Infrastructure.EntityFramwerork;

namespace ZaklepTo.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ZaklepToContext _context;

        public EmployeeRepository(ZaklepToContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string login)
        {
            var employeeToRemove = await _context.Employees.FindAsync(login);
            _context.Employees.Remove(employeeToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
            => await _context.Employees.ToListAsync();

        public async Task<Employee> GetAsync(string login)
            => await _context.Employees.FindAsync(login);

        public async Task UpdateAsync(Employee employee)
        {
            _context.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}
