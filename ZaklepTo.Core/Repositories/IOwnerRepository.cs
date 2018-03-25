using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZaklepTo.Core.Domain;

namespace ZaklepTo.Core.Repositories
{
    public interface IOwnerRepository
    {
        Task<Owner> GetAsync(string login);
        Task<IEnumerable<Owner>> GetAllAsync();
        Task AddAsync(Owner owner);
        Task UpdateAsync(Owner owner);
        Task DeleteAsync(string login);
    }
}