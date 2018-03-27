using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZaklepTo.Infrastucture.DTO;

namespace ZaklepTo.Infrastucture.Services.Implementations
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantDTO>> GetAllAsync();
        Task<RestaurantDTO> GetAsync(Guid id);
        Task UpdateAsync(RestaurantDTO restaurantDto);
    }
}