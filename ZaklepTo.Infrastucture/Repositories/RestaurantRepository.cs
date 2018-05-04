using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZaklepTo.Core.Domain;
using ZaklepTo.Core.Repositories;
using ZaklepTo.Infrastructure.EntityFramwerork;

namespace ZaklepTo.Infrastructure.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly ZaklepToContext _context;

        public RestaurantRepository(ZaklepToContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Restaurant restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid restaurantId)
        {
            var restaurantToRemove = await _context.Restaurants.FindAsync(restaurantId);
            _context.Restaurants.Remove(restaurantToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
            => await _context.Restaurants
                .Include(x => x.Tables)
                .ToListAsync();

        public async Task<Restaurant> GetAsync(Guid restaurantId)
            => await _context.Restaurants
                .Include(x => x.Tables)
                .SingleOrDefaultAsync(x => x.Id == restaurantId);

        public async Task UpdateAsync(Restaurant restaurant)
        {
            _context.Update(restaurant);
            await _context.SaveChangesAsync();
        }
    }
}
