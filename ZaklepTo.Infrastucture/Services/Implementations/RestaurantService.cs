using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ZaklepTo.Core.Domain;
using ZaklepTo.Core.Exceptions;
using ZaklepTo.Core.Repositories;
using ZaklepTo.Infrastructure.DTO;
using ZaklepTo.Infrastructure.DTO.OnCreate;
using ZaklepTo.Infrastructure.DTO.OnUpdate;
using ZaklepTo.Infrastructure.Services.Interfaces;

namespace ZaklepTo.Infrastructure.Services.Implementations
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }

        public async Task DeleteAsync(Guid restaurantId)
        {
            var restaurant = await _restaurantRepository.GetAsync(restaurantId);
            if (restaurant == null)
                throw new ServiceException(ErrorCodes.RestaurantNotFound, "Restaurant doesn't exist.");

            await _restaurantRepository.DeleteAsync(restaurantId);
        }

        public async Task<IEnumerable<RestaurantDto>> GetAllAsync()
        {
            var restaurants = await _restaurantRepository.GetAllAsync();
            return restaurants.Select(restaurant => _mapper.Map<Restaurant, RestaurantDto>(restaurant));
        }

        public async Task<RestaurantDto> GetAsync(Guid restaurantId)
        {
            var restaurant = await _restaurantRepository.GetAsync(restaurantId);
            if (restaurant == null)
                throw new ServiceException(ErrorCodes.RestaurantNotFound, "Restaurant doesn't exist.");

            return _mapper.Map<Restaurant, RestaurantDto>(restaurant);
        }

        public async Task RegisterAsync(RestaurantOnCreateDto restaurantDto)
        {
            var restaurantToRegister = new Restaurant(restaurantDto.Name, restaurantDto.Description,
                restaurantDto.Cuisine, restaurantDto.RepresentativePhotoUrl, restaurantDto.Localization, restaurantDto.Tables);

            await _restaurantRepository.AddAsync(restaurantToRegister);
        }

        public async Task UpdateAsync(RestaurantOnUpdateDto restaurantDto, Guid restaurantId)
        {
            var restaurantToUpdate = await _restaurantRepository.GetAsync(restaurantId);
            if(restaurantToUpdate == null)
                throw new ServiceException(ErrorCodes.RestaurantNotFound, "Restaurant doesn't exist.");

            restaurantToUpdate.Name = restaurantDto.Name;
            restaurantToUpdate.Description = restaurantDto.Description;
            restaurantToUpdate.Cuisine = restaurantDto.Cuisine;
            restaurantToUpdate.Localization = restaurantDto.Localization;
            restaurantToUpdate.RepresentativePhotoUrl = restaurantDto.RepresentativePhotoUrl;
            restaurantToUpdate.Tables = restaurantDto.Tables;

            await _restaurantRepository.UpdateAsync(restaurantToUpdate);
        }
    }
}
