using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZaklepTo.Infrastucture.DTO;
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
        public async Task<IActionResult> GetAllRestaurant()
        {
            IEnumerable<RestaurantDTO> restaurants = await _restaurantService.GetAllAsync();

            return Ok(restaurants);
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetRestaurant(Guid guid)
        {
            var restaurantDTO = await _restaurantService.GetAsync(guid);

            if (restaurantDTO == null)
                return NotFound();

            return Ok(restaurantDTO);
        }

        [HttpPut("{guid}/update")]
        public async Task<IActionResult> UpdateRestaurant([FromBody] RestaurantDTO updatedRestaurant)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var restaurantToUpdate = await _restaurantService.GetAsync(updatedRestaurant.Id);

            if (restaurantToUpdate == null)
                return BadRequest();

            restaurantToUpdate.Id = updatedRestaurant.Id;
            restaurantToUpdate.Name = updatedRestaurant.Name;
            restaurantToUpdate.Description = updatedRestaurant.Description;
            restaurantToUpdate.Cuisine = updatedRestaurant.Cuisine;
            restaurantToUpdate.Tables = updatedRestaurant.Tables;

            return Ok();
        }
    }
}