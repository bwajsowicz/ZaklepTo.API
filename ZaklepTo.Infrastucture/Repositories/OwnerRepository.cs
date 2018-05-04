using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZaklepTo.Core.Domain;
using ZaklepTo.Core.Repositories;
using ZaklepTo.Infrastructure.EntityFramwerork;

namespace ZaklepTo.Infrastructure.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly ZaklepToContext _context;

        public OwnerRepository(ZaklepToContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Owner owner)
        {
            await _context.Owners.AddAsync(owner);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string login)
        {
            var ownerToRemove = await _context.Owners.FindAsync(login);
           _context.Owners.Remove(ownerToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Owner>> GetAllAsync()
            => await _context.Owners
                .Include(x => x.Restaurant)
                .ToListAsync();

        public async Task<Owner> GetAsync(string login)
            => await _context.Owners
                .Include(x => x.Restaurant)
                .SingleOrDefaultAsync(x => x.Login == login);

        public async Task UpdateAsync(Owner owner)
        {
            _context.Update(owner);
            await _context.SaveChangesAsync();
        }
    }
}
