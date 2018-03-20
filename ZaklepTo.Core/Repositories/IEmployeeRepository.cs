using System.Collections.Generic;
using System.Threading.Tasks;
using ZaklepTo.Core.Domain;

namespace ZaklepTo.Core.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetAsync(string email);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(string email);
    }
}