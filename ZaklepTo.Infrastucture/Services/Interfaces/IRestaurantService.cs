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
        Task<RestaurantDto> GetAsync(Guid restaurantId);
        Task UpdateAsync(RestaurantOnUpdateDto restaurantDto, Guid restaurantId);
        Task DeleteAsync(Guid restaurantId);
        Task RegisterAsync(RestaurantOnCreateDto restaurantDto);
    }
}