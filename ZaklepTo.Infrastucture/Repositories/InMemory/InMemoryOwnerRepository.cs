using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZaklepTo.Core.Domain;
using ZaklepTo.Core.Repositories;

namespace ZaklepTo.Infrastructure.Repositories.InMemory
{
    public class InMemoryOwnerRepository : IOwnerRepository
    { 
        private static readonly ISet<Owner> _owners = new HashSet<Owner>();

        public async Task AddAsync(Owner owner)
            => await Task.FromResult(_owners.Add(owner));

        public async Task DeleteAsync(string login)
        {
            var owner = await GetAsync(login);
            _owners.Remove(owner);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Owner>> GetAllAsync()
            => await Task.FromResult(_owners);

        public async Task<Owner> GetAsync(string login)
            => await Task.FromResult(_owners.SingleOrDefault(x => x.Login == login));

        public async Task UpdateAsync(Owner owner)
        {
            var oldOwner = await GetAsync(owner.Login);
            _owners.Remove(oldOwner);
            _owners.Add(owner);
            await Task.CompletedTask;
        }
    }
}
