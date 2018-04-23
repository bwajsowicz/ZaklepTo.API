using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ZaklepTo.Core.Domain;
using ZaklepTo.Core.Exceptions;
using ZaklepTo.Core.Repositories;
using ZaklepTo.Infrastructure.DTO;
using ZaklepTo.Infrastructure.DTO.EntryData;
using ZaklepTo.Infrastructure.DTO.OnCreate;
using ZaklepTo.Infrastructure.DTO.OnUpdate;
using ZaklepTo.Infrastructure.Encrypter;
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

        public async Task DeleteAsync(Guid id)
        {
            await _restaurantRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<RestaurantDTO>> GetAllAsync()
        {
            var restaurants = await _restaurantRepository.GetAllAsync();
            return restaurants.Select(restaurant => _mapper.Map<Restaurant, RestaurantDTO>(restaurant));
        }

        public async Task<RestaurantDTO> GetAsync(Guid id)
        {
            var restaurant = await _restaurantRepository.GetAsync(id);
            if (restaurant == null)
                throw new ServiceException(ErrorCodes.RestaurantNotFound, "Restaurant doesn't exist.");
            return _mapper.Map<Restaurant, RestaurantDTO>(restaurant);
        }

        public async Task RegisterAsync(RestaurantOnCreateDTO restaurantDto)
        {
            var restaurantToRegister = new Restaurant(restaurantDto.Name, restaurantDto.Description,
                restaurantDto.Cuisine, restaurantDto.Localization, restaurantDto.Tables);

            await _restaurantRepository.AddAsync(restaurantToRegister);
        }

        public async Task UpdateAsync(RestaurantOnUpdateDTO restaurantDto)
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}
