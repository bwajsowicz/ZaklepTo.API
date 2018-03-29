using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZaklepTo.Infrastucture.DTO;
using ZaklepTo.Infrastucture.DTO.OnUpdate;
using ZaklepTo.Infrastucture.Services.Interfaces;

namespace ZaklepTo.API.Controllers
{
    [Route("api/restaurants")]
    public class RestaurantController : Controller
    {
        private IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllRestaurants()
        {
            IEnumerable<RestaurantDTO> restaurants = await _restaurantService.GetAllAsync();

            return Ok(restaurants);
        }

        [HttpGet("{restaurantId}")]
        public async Task<IActionResult> GetSingleRestaurant(Guid restaurantId)
        {
            var restaurant = await _restaurantService.GetAsync(restaurantId);

            if (restaurant == null)
                return NotFound();

            return Ok(restaurant);
        }

        [HttpPut("{restaurantId}/update")]
        public async Task<IActionResult> UpdateRestaurant([FromBody] RestaurantOnUpdateDTO updatedRestaurant)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var restaurantToUpdate = await _restaurantService.GetAsync(updatedRestaurant.Id);

            if (restaurantToUpdate == null)
                return NotFound();

            await _restaurantService.UpdateAsync(updatedRestaurant);

            return Ok();
        }
    }
}