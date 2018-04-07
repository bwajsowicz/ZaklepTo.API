using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ZaklepTo.Infrastructure.DTO;
using ZaklepTo.Infrastructure.Services.Interfaces;
using ZaklepTo.Infrastructure.DTO.OnCreate;

namespace ZaklepTo.Infrastructure.Services.Implementations
{
    public class DataDataInitializer : IDataInitializer
    {
        private readonly ICustomerService _customerService;
        private readonly IEmployeeService _employeeService;
        private readonly IOwnerService _ownerService;
        private readonly IReservationService _reservationService;
        private readonly IRestaurantService _restaurantService;

        public DataDataInitializer(ICustomerService customerService, IEmployeeService employeeService, 
            IOwnerService ownerService, IReservationService reservationService, IRestaurantService restaurantService)
        {
            _customerService = customerService;
            _employeeService = employeeService;
            _ownerService = ownerService;
            _restaurantService = restaurantService;
            _reservationService = reservationService;
        }

        public async Task SeedAsync()
        {
            var random = new Random();

            List<string> ExampleFirstName = new List<string> 
            {
                "Adam",
                "Kamil",
                "Bartosz",
                "Jorge",
                "Jacek",
                "Karol",
                "Krzystof"
            };

            List<string> ExampleLastName = new List<string>
            {
                "Kowalski",
                "Nowak",
                "Wojtyła",
                "Lopez",
                "Duda",
                "Wałęsa",
                "Jotaro"
            };

            for(var i = 1; i < 10; i++)
            {
                var firstName = ExampleFirstName[random.Next(ExampleFirstName.Count)];
                var lastName = ExampleLastName[random.Next(ExampleLastName.Count)];
                var login = firstName + i.ToString();
                var email = $"{login}@gmail.com";
                var phone = $"{i}02345678";

                CustomerOnCreateDTO customer = new CustomerOnCreateDTO()
                {
                    Login = login,
                    FirstName = firstName,
                    Email = email,
                    Phone = phone,
                    LastName = lastName
                };

                await _customerService.RegisterAsync(customer);
            }

        }
    }
}
