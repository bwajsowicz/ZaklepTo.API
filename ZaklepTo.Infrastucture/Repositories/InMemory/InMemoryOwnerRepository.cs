using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZaklepTo.Core.Domain;
using ZaklepTo.Core.Repositories;

namespace ZaklepTo.Infrastructure.Repositories.InMemory
{
    public class InMemoryOwnerRepository : IOwnerRepository
    { 
        //nulls
        private static readonly ISet<Owner> _owners = new HashSet<Owner>
        {
            new Owner("own1", "Jan", "Kowalski", "jkowalski@example.com", "123-123-123", "pass1", "salt", null),
            new Owner("own2", "Zbigniew", "Phrymus", "jphrymus@example.com", "321-321-321", "pass2", "salt", null),
            new Owner("own3", "Zbigniew", "Huston", "jhuston@example.com", "321-555-321", "pass3", "salt", null)
        };

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
