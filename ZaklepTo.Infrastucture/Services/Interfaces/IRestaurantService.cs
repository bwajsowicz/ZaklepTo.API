using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZaklepTo.Infrastructure.DTO;
using ZaklepTo.Infrastructure.DTO.OnUpdate;
using ZaklepTo.Infrastructure.DTO.OnCreate;

namespace ZaklepTo.Infrastructure.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantDto>> GetAllAsync();
        Task<RestaurantDto> GetAsync(Guid id);
        Task UpdateAsync(RestaurantOnUpdateDto restaurantDto);
        Task DeleteAsync(Guid id);
        Task RegisterAsync(RestaurantOnCreateDto restaurantDto);
    }
}