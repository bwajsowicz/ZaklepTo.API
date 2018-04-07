using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ZaklepTo.Core.Domain;
using ZaklepTo.Infrastructure.DTO;
using ZaklepTo.Infrastructure.DTO.OnCreate;
using ZaklepTo.Infrastructure.Services.Interfaces;

namespace ZaklepTo.Infrastructure.Services
{
    public class DataDataInitializer : IDataInitializer
    {
        private readonly ICustomerService customerService;
        private readonly IEmployeeService employeeService;
        private readonly IOwnerService ownerService;
        private readonly IReservationService reservationService;
        private readonly IRestaurantService restaurantService;

        public DataDataInitializer(ICustomerService customerService, IEmployeeService employeeService, 
            IOwnerService ownerService, IReservationService reservationService, IRestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
            this.reservationService = reservationService;
            this.customerService = customerService;
            this.employeeService = employeeService;
            this.ownerService = ownerService;
        }

        public async Task SeedAsync()
        {
           IEnumerable<CustomerDTO> customers = await customerService.GetAllAsync();
           if (customers.Any())
                return;
           

            var random = new Random();

            List<string> ExampleFirstName = new List<string>();
            {
                ExampleFirstName.Add("Adam");
                ExampleFirstName.Add("Kamil");
                ExampleFirstName.Add("Bartosz");
                ExampleFirstName.Add("Jorge");
                ExampleFirstName.Add("Jacek");
                ExampleFirstName.Add("Karol");
                ExampleFirstName.Add("Krzysztof");
            };

            List<string> ExampleLastName = new List<string>();
            {
                ExampleLastName.Add("Kowalski");
                ExampleLastName.Add("Nowak");
                ExampleLastName.Add("Wojtyla");
                ExampleLastName.Add("Lopez");
                ExampleLastName.Add("Duda");
                ExampleLastName.Add("Walesa");
                ExampleLastName.Add("Tusk");
            };

            for(var i = 1; i < 10; i++)
            {
                var firstName = random.Next(ExampleFirstName.Count);
                var lastName = random.Next(ExampleLastName.Count);
                var login = firstName + i.ToString();
                var email = $"{login}@gmail.com";
                var phone = $"{i}02345678";

                //Need to add await RegisterAsync
            }

        }
    }
}
