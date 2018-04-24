using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZaklepTo.Core.Domain;
using ZaklepTo.Core.Repositories;

namespace ZaklepTo.Infrastructure.Repositories.InMemory
{
    public class InMemoryOwnerRepository : IOwnerRepository
    { 
        private static readonly ISet<Owner> Owners = new HashSet<Owner>();

        public async Task AddAsync(Owner owner)
            => await Task.FromResult(Owners.Add(owner));

        public async Task DeleteAsync(string login)
        {
            var owner = await GetAsync(login);
            Owners.Remove(owner);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Owner>> GetAllAsync()
            => await Task.FromResult(Owners);

        public async Task<Owner> GetAsync(string login)
            => await Task.FromResult(Owners.SingleOrDefault(x => x.Login == login));

        public async Task UpdateAsync(Owner owner)
        {
            var oldOwner = await GetAsync(owner.Login);
            Owners.Remove(oldOwner);
            Owners.Add(owner);
            await Task.CompletedTask;
        }
    }
}
