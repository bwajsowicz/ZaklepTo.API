using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZaklepTo.Core.Exceptions;
using ZaklepTo.Infrastructure.DTO.EntryData;
using ZaklepTo.Infrastructure.DTO.OnCreate;
using ZaklepTo.Infrastructure.DTO.OnUpdate;
using ZaklepTo.Infrastructure.Services.Interfaces;

namespace ZaklepTo.API.Controllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllAsync();

            return Ok(customers);
        }

        [HttpGet("{login}")]
        public async Task<IActionResult> GetSingleCustomer(string login)
        {
            var customer = await _customerService.GetAsync(login);

            return Ok(customer);
        }  

        [HttpGet("{login}/toprestaurants")]
        public async Task<IActionResult> GetCustomersTopRestaurants(string login)
        {
            var topRestaurantsForCustomer = 
                await _customerService.GetMostFrequentRestaurants(login);

            return Ok(topRestaurantsForCustomer);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterCustomer([FromBody] CustomerOnCreateDTO customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _customerService.RegisterAsync(customer);

            return Created($"{customer.Login}", Json(customer));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginCustomer([FromBody] LoginCredentials loginCredentials)
        {
            await _customerService.LoginAsync(loginCredentials);

            return Ok();
        }

        [HttpPut("{login}/update")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerOnUpdateDTO updatedCustomer)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await _customerService.UpdateAsync(updatedCustomer);

            return Ok();
        }

        [HttpPut("{login}/changepassword")]
        public async Task<IActionResult> ChangeCustomersPassword([FromBody] PasswordChange passwordChange)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            await _customerService.ChangePassword(passwordChange);

            return Ok();
        }
        
        [HttpDelete("{login}/remove")]
        public async Task<IActionResult> RemoveCustomer(string login)
        {
            await _customerService.DeleteAsync(login);

            return Ok();
        }
    }
}