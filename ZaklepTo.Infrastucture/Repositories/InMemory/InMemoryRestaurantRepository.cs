using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZaklepTo.Core.Domain;
using ZaklepTo.Core.Repositories;

namespace ZaklepTo.Infrastructure.Repositories.InMemory
{
    public class InMemoryRestaurantRepository : IRestaurantRepository
    {
        private static readonly ISet<Restaurant> Restaurants = new HashSet<Restaurant>();

        public async Task AddAsync(Restaurant restaurant)
            => await Task.FromResult(Restaurants.Add(restaurant));

        public async Task DeleteAsync(Guid id)
        {
            var restaurant = await GetAsync(id);
            Restaurants.Remove(restaurant);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
            => await Task.FromResult(Restaurants);

        public async Task<Restaurant> GetAsync(Guid id)
            => await Task.FromResult(Restaurants.SingleOrDefault(x => x.Id == id));

        public async Task UpdateAsync(Restaurant restaurant)
        {
            var oldRestaurant = await GetAsync(restaurant.Id);
            Restaurants.Remove(oldRestaurant);
            Restaurants.Add(restaurant);
            await Task.CompletedTask;
        }
    }
}
