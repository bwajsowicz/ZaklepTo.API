using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ZaklepTo.Infrastructure.DTO;
using ZaklepTo.Infrastructure.Services.Interfaces;
using ZaklepTo.Infrastructure.DTO.OnCreate;
using ZaklepTo.Infrastructure.Mappers;
using AutoMapper;
using ZaklepTo.Core.Domain;

namespace ZaklepTo.Infrastructure.Services.Implementations
{
    public class DataInitializer : IDataInitializer
    {
        private readonly ICustomerService _customerService;
        private readonly IEmployeeService _employeeService;
        private readonly IOwnerService _ownerService;
        private readonly IReservationService _reservationService;
        private readonly IRestaurantService _restaurantService;
        private readonly IMapper _mapper;

        public DataInitializer(ICustomerService customerService, IEmployeeService employeeService, 
            IOwnerService ownerService, IReservationService reservationService, IRestaurantService restaurantService, IMapper mapper)
        {
            _customerService = customerService;
            _employeeService = employeeService;
            _ownerService = ownerService;
            _restaurantService = restaurantService;
            _mapper = mapper;
            _reservationService = reservationService;
        }

        public async Task SeedAsync()
        {
            var random = new Random();

            var exampleFirstName = new List<string>
            {
                "Adam",
                "Kamil",
                "Bartosz",
                "Jorge",
                "Jacek",
                "Karol",
                "Krzystof"
            };

            var exampleLastName = new List<string>
            {
                "Kowalski",
                "Nowak",
                "Wojtyła",
                "Lopez",
                "Duda",
                "Wałęsa",
                "Jotaro"
            };

            for (var i = 1; i < 10; i++)
            {
                var firstName = exampleFirstName[random.Next(exampleFirstName.Count)];
                var lastName = exampleLastName[random.Next(exampleLastName.Count)];
                var login = $"customer{i}";
                var email = $"{login}@gmail.com";
                var phone = $"{i}02345678";

                var customer = new CustomerOnCreateDto()
                {
                    Login = login,
                    FirstName = firstName,
                    Email = email,
                    Phone = phone,
                    LastName = lastName,
                    Password = "!QAZxsw2"
                };

                await _customerService.RegisterAsync(customer);
            } //Customer

            var exampleRestaurantName = new List<string>
            {
                "Ceglana",
                "Spizarnia",
                "Bachus",
                "Ole_Bistro",
                "Brazileriro",
                "Kawusia",
                "Bezka",
                "Kicia",
                "Merszmyl",
                "Mordaler"
            };

            var exampleCousine = new List<string>
            {
                "Chinska",
                "Hiszpanska",
                "Polska",
                "Wloska",
                "Wloska"
            };

            for (var i = 0; i <= exampleRestaurantName.Count(); i++)
            {
                var tables = new List<Table>(); 
                for (var j = 0; j < random.Next(5, 12); j++)
                {
                    var exampleTable = new Table(random.Next(1, 10), (random.Next(1, 100), random.Next(1, 100)));

                    tables.Add(exampleTable);
                } // Tables
                var restaurant = new RestaurantOnCreateDto()
                {
                    Name = exampleRestaurantName[i],
                    Description = "Description",
                    Cuisine = exampleCousine[random.Next(1, exampleCousine.Count())],
                    Localization = $"Szczecin{i}",
                    Tables = tables
                }; // Restaurant

                await _restaurantService.RegisterAsync(restaurant);

                var restaurantDto = (await _restaurantService.GetAllAsync())
                    .Last();

                for (var z = 1; z <= 10; z++)
                {
                    var firstNameEmployee = exampleFirstName[random.Next(exampleFirstName.Count)];
                    var lastNameEmployee = exampleLastName[random.Next(exampleLastName.Count)];
                    var loginEmployee = $"employee{i}{z}";
                    var emailEmployee = $"{loginEmployee}@gmail.com";
                    var phoneEmployee = $"{i}{z}02345678";

                    EmployeeOnCreateDto employee = new EmployeeOnCreateDto()
                    {
                        Login = loginEmployee,
                        FirstName = firstNameEmployee,
                        Email = emailEmployee,
                        Phone = phoneEmployee,
                        LastName = lastNameEmployee,
                        Password = "!QAZxsw2",
                        Restaurant = restaurantDto
                    };

                    await _employeeService.RegisterAsync(employee);
                } //Employee

                var firstNameOwner = exampleFirstName[random.Next(exampleFirstName.Count)]; //Owner
                var lastNameOwner = exampleLastName[random.Next(exampleLastName.Count)];
                var loginOwner = $"owner{i}";
                var emailOwner = $"{loginOwner}@gmail.com";
                var phoneOwner = $"{i}02345678";

                var owner = new OwnerOnCreateDto()
                {
                    Login = loginOwner,
                    FirstName = firstNameOwner,
                    Email = emailOwner,
                    Phone = phoneOwner,
                    LastName = lastNameOwner,
                    Password = "!QAZxsw2",
                    Restaurant = restaurantDto
                };

                await _ownerService.RegisterAsync(owner);

                
                for (var z = 0; z < tables.Count()/2; z++)
                {
                    var date = new DateTime(2018, random.Next(1, 12), random.Next(1, 27), random.Next(1, 24), random.Next(1, 60), 0);
                    var reservation = new ReservationOnCreateDto()
                    {
                        Restaurant = restaurantDto,
                        Table = _mapper.Map<Table, TableDto>(tables.ElementAt(z)),
                        DateStart = date,
                        DateEnd = date.AddHours(3),
                        Customer =  (await _customerService.GetAllAsync()).ElementAt(z)              
                    };

                    await _reservationService.RegisterReservation(reservation);
                }              
            } //Restaurant & Owner & Employee
       }
    }
}
