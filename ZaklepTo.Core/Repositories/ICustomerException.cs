using System.Collections.Generic;
using System.Threading.Tasks;
using ZaklepTo.Core.Domain;

namespace ZaklepTo.Core.Repositories
{
    public interface ICustomerException
    {
        Task<Customer> GetAsync(string email);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(string email);
    }
}