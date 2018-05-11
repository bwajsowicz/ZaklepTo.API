using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZaklepTo.Infrastructure.DTO.OnUpdate;
using ZaklepTo.Infrastructure.Services.Interfaces;

namespace ZaklepTo.API.Controllers
{
    [Route("api/restaurants")]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var restaurants = await _restaurantService.GetAllAsync();

            return Ok(restaurants);
        }

        [HttpGet("{restaurantId}")]
        public async Task<IActionResult> GetSingleRestaurant(Guid restaurantId)
        {
            var restaurant = await _restaurantService.GetAsync(restaurantId);

            return Ok(restaurant);
        }

        [HttpPut("{restaurantId}/update")]
        public async Task<IActionResult> UpdateRestaurant([FromBody] RestaurantOnUpdateDto updatedRestaurant, Guid restaurantId)
        {
            if (!ModelState.IsValid)
                return StatusCode(420, ModelState);

            await _restaurantService.UpdateAsync(updatedRestaurant, restaurantId);

            return Ok();
        }

        [HttpDelete("{restaurantId}")]
        public async Task<IActionResult> RemoveRestaurant(Guid restaurantId)
        {
            await _restaurantService.DeleteAsync(restaurantId);

            return Ok();
        }
    }
}