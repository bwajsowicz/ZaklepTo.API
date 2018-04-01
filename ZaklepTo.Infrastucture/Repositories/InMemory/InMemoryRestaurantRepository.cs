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
        //nulls
        private readonly ISet<Restaurant> _restaurants = new HashSet<Restaurant>
        {
            new Restaurant("Testowa1", "Opis", "Dobra", "Szczecin", null),
            new Restaurant("Testowa2", "Opis", "Polska", "Szczecin", null),
            new Restaurant("Testowa2", "Opis", "Śląska", "Szczecin", null)
        };

        public async Task AddAsync(Restaurant restaurant)
            => await Task.FromResult(_restaurants.Add(restaurant));

        public async Task DeleteAsync(Guid id)
        {
            var restaurant = await GetAsync(id);
            _restaurants.Remove(restaurant);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
            => await Task.FromResult(_restaurants);

        public async Task<Restaurant> GetAsync(Guid id)
            => await Task.FromResult(_restaurants.SingleOrDefault(x => x.Id == id));

        public async Task UpdateAsync(Restaurant restaurant)
        {
            var oldRestaurant = await GetAsync(restaurant.Id);
            _restaurants.Remove(oldRestaurant);
            _restaurants.Add(restaurant);
            await Task.CompletedTask;
        }
    }
}
