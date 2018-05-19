using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using ZaklepTo.Infrastructure.DTO;
using ZaklepTo.Infrastructure.Services.Interfaces;
using ZaklepTo.Infrastructure.DTO.OnCreate;
using AutoMapper;
using ZaklepTo.Core.Domain;
using ZaklepTo.Infrastructure.DTO.OnUpdate;

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
                "Oskar",
                "Jacek",
                "Karol",
                "Krzystof",
                "Antoni",
                "Jakub",
                "Jan",
                "Szymon",
                "Franciszek",
                "Filip",
                "Aleksander",
                "Mikołaj",
                "Wojciech",
                "Kacper",
                "Michał",
                "Marcel",
                "Stanisław",
                "Wiktor",
                "Piotr",
                "Igor",
                "Leon",
                "Mateusz",
                "Maksymilian",
                "Miłosz",
                "Oliwier",
                "Tomasz",
                "Karol"
            };

            var exampleLastName = new List<string>
            {
                "Kowalski",
                "Nowak",
                "Lewandowski",
                "Jankowski",
                "Dąbrowski",
                "Kaczmarek",
                "Stępień",
                "Wiśniewski",
                "Kowalczyk",
                "Wójcik",
                "Kamiński",
                "Zieliński",
                "Mielnik",
                "Mazur",
                "Kwiatkowski",
                "Krawczyk",
                "Chyliński",
                "Dąbrowski",
                "Zając",
                "Michalski",
                "Pawłowski",
                "Wróbel",
                "Jabłoński",
                "Wieczorek",
                "Nowakowski",
                "Olszewski",
                "Adamczyk",
                "Pawlak",
                "Sikora",
                "Śpiewak",
            };

            // Customer
            var customers = await _customerService.GetAllAsync();
            if (!customers.Any())
            {
                for (var i = 0; i < 100; i++)
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
                        Password = "qwerty1"
                    };

                    await _customerService.RegisterAsync(customer);
                }
            }

            var exampleRestaurantName = new List<string>
            {
                "Ceglana",
                "Spiżarnia",
                "Hospoda",
                "Ole Bistro",
                "Brazileriro",
                "Kawusia",
                "Zamkowa",
                "Pod Kotem",
                "Rewers",
                "Taberna del Puerto"
            };

            var exampleCousine = new List<string>
            {
                "Hiszpańska",
                "Polska",
                "Włoska",
                "Chińska"
            };

            // Tables
            var tables = new List<Table>();
            for (var j = 0; j < 100; j++)
            {
                var table = new Table(random.Next(1, 5), (0, 0));
                tables.Add(table);
            }

            // Restaurants
            var restaurants = (await _restaurantService.GetAllAsync()).ToList();
            if (!restaurants.Any())
            {
                for (var i = 0; i < 10; i++)
                {
                    var restaurantTables = new List<Table>();
                    for (var j = 0; j < 10; j++)
                    {
                        restaurantTables.Add(tables.ElementAt(j + (i * 10)));
                    }

                    var restaurant = new RestaurantOnCreateDto()
                    {
                        Name = exampleRestaurantName[i],
                        Description =
                            $@"Restauracja {
                                    exampleRestaurantName[i]
                                } jest od lat cenionym miejscem kameralnych spotkań, znanym ze znakomitej kuchni. Zapraszamy na  wyśmienity lunch w przerwie w pracy, wykwintny obiad z rodziną lub romantyczny wieczór na tarasach przy lampce wina.  Jadłospis zawiera wiele zarówno tradycyjnych jak również typowo europejskich dań.

                    Szef kuchni szczególnie poleca - kotlet a'la {
                                    exampleRestaurantName[i]
                                }, maczankę firmową (schab w sosie myśliwskim z grzankami), kotlet Pani Walewska, naleśniki a'la {
                                    exampleRestaurantName[i]
                                }. Wielu 'zwolenników' ma firmowa szarlotka z bitą śmietaną,
                    a także banany w sosie rumowym chętnie spożywane jako dodatek do kawy.


                    Szczególnie warte polecenia są również wina serwowane w restauracji.

                    W restauracji hotelu Maria odbywają się huczne wesela, przyjęcia okolicznościowe, bankiety firmowe. sylwestry.",
                        Cuisine = exampleCousine[random.Next(0, exampleCousine.Count - 1)],
                        Localization = $"Szczecin",
                        Tables = restaurantTables
                    };

                    await _restaurantService.RegisterAsync(restaurant);
                }
                restaurants = (await _restaurantService.GetAllAsync()).ToList();
                // Restaurant Photo               
                for (var i = 0; i < 10; i++)
                {
                    var restaurant = restaurants.ElementAt(i);
                    var restaurantGuid = restaurant.Id;
                    File.Copy($"wwwroot/images/datainitializer/examplephotos/0{random.Next(0, 9)}.jpg", $"wwwroot/images/restaurants/{restaurantGuid}.jpg");
                    restaurant.RepresentativePhotoUrl =
                        $"http://zakleptoapi.azurewebsites.net/images/restaurants/{restaurantGuid}.jpg";

                    var restaurantWithPhoto = _mapper.Map<RestaurantDto, RestaurantOnUpdateDto>(restaurant);
                    await _restaurantService.UpdateAsync(restaurantWithPhoto, restaurant.Id);

                }
            }

            // Employees
            var employees = await _employeeService.GetAllAsync();
            if(!employees.Any())
            {
                restaurants = (await _restaurantService.GetAllAsync()).ToList();
                for (var i = 0; i < 10; i++)
                {
                    for (var j = 1; j <= 10; j++)
                    {
                        var employee = new EmployeeOnCreateDto()
                        {
                            Login = $"employee{i}{j}",
                            FirstName = exampleFirstName[random.Next(exampleFirstName.Count)],
                            LastName = exampleLastName[random.Next(exampleLastName.Count)],
                            Email = $"employee{i}{j}@gmail.com",
                            Phone = $"{i}{j}2345678",
                            Password = "qwerty1",
                            Restaurant = restaurants.ElementAt(i)
                        };

                        await _employeeService.RegisterAsync(employee);
                    }
                }
            }

            // Owners
            var owners = await _ownerService.GetAllAsync();
            if(!owners.Any())
            {     
                for (var i = 1; i <= 10; i++)
                {
                    var owner = new OwnerOnCreateDto()
                    {
                        Login = $"owner{i}",
                        FirstName = exampleFirstName[random.Next(exampleFirstName.Count)],
                        LastName = exampleLastName[random.Next(exampleLastName.Count)],
                        Email = $"owner{i}@gmail.com",
                        Phone = $"{i}32345678",
                        Password = "qwerty1",
                        Restaurant = restaurants.ElementAt(i-1)
                    };

                    await _ownerService.RegisterAsync(owner);
                }
            }

            // Reservations
            var reservations = await _reservationService.GetAllAsync();
            if(!reservations.Any())
            { 
                var registeredCustomers = (await _customerService.GetAllAsync()).ToList();
        
                for (var i = 0; i < 10; i++)
                {
                    for (var j = 0; j < random.Next(5, 10); j++)
                    {
                        var restaurantId = random.Next(0, 9);
                        var date = new DateTime(2018, random.Next(1, 12), random.Next(1, 27), random.Next(1, 24), random.Next(1, 60), 0);
                        var reservation = new ReservationOnCreateDto()
                        {
                            Restaurant = restaurants.ElementAt(restaurantId),
                            TableId = restaurants.ElementAt(restaurantId).Tables.ElementAt(random.Next(0,9)).Id,
                            DateStart = date,
                            DateEnd = date.AddHours(1),
                            Customer = registeredCustomers.ElementAt(random.Next(0, 99)),
                        };

                        await _reservationService.RegisterReservation(reservation);
                    }
                }
            }
                      
            // Reservation Confirm
            foreach (var reservationDto in await _reservationService.GetAllAsync())
            {
                if (random.Next(1, 10) > 3)
                    await _reservationService.ConfirmReservationAsync(reservationDto.Id);
            }
        }          
    }
}
