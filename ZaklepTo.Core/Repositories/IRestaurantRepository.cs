using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZaklepTo.Core.Domain;

namespace ZaklepTo.Core.Repositories
{
    public interface IRestaurantRepository
    {
        Task<Restaurant> GetAsync(Guid id);
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task AddAsync(Restaurant restaurant);
        Task UpdateAsync(Restaurant restaurant);
        Task DeleteAsync(Guid id);
    }
}