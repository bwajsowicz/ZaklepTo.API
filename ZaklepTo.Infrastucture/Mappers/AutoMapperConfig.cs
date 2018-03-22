using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ZaklepTo.Core.Domain;
using ZaklepTo.Infrastucture.DTO;

namespace ZaklepTo.Infrastucture.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Customer, CustomerDTO>();
                cfg.CreateMap<Employee, EmployeeDTO>();
                cfg.CreateMap<Owner, OwnerDTO>();
                cfg.CreateMap<Reservation, ReservationDTO>();
                cfg.CreateMap<Restaurant, RestaurantDTO>();
                cfg.CreateMap<Table, TableDTO>();
            })
            .CreateMapper();
    }
}
