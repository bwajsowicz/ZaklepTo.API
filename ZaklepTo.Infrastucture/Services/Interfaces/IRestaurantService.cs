﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZaklepTo.Infrastucture.DTO;
using ZaklepTo.Infrastucture.DTO.OnUpdate;

namespace ZaklepTo.Infrastucture.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantDTO>> GetAllAsync();
        Task<RestaurantDTO> GetAsync(Guid id);
        Task UpdateAsync(RestaurantOnUpdateDTO restaurantDto);
    }
}