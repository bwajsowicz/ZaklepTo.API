using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZaklepTo.Infrastucture.Services.Implementations;
using AutoMapper;
using ZaklepTo.Core.Domain;
using ZaklepTo.Infrastucture.DTO;

namespace ZaklepTo.API.Controllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        private ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllCustomers()
        {
            IEnumerable<CustomerDTO> customers = await _customerService.GetAllAsync();

            return Ok(customers);
        }

        [HttpGet("{login}")]
        public async Task<IActionResult> GetCustomer(string login)
        {
            var customerDTO = await _customerService.GetAsync(login);

            if (customerDTO == null)
                return NotFound();

            return Ok(customerDTO);
        }  

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CustomerOnCreateDTO customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _customerService.RegisterAsync(
                customer.Login,
                customer.FirstName,
                customer.LastName,
                customer.Email,
                customer.Phone,
                customer.Password
                );

            return Created($"{customer.Login}", Json(customer));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] CustomerOnUpdateDTO updatedCustomer)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var customerToUpdate = await _customerService.GetAsync(updatedCustomer.Login);

            if (customerToUpdate == null)
                return BadRequest();

            // TODO: password update.

            customerToUpdate.Login = updatedCustomer.Login;
            customerToUpdate.FirstName = updatedCustomer.FirstName;
            customerToUpdate.LastName = updatedCustomer.LastName;
            customerToUpdate.Email = updatedCustomer.Email;
            customerToUpdate.Phone = updatedCustomer.Phone;

            return Ok();
        }
    }
}