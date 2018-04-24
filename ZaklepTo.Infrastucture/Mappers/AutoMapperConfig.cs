using AutoMapper;
using ZaklepTo.Core.Domain;
using ZaklepTo.Infrastructure.DTO;

namespace ZaklepTo.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Customer, CustomerDto>();
                cfg.CreateMap<Employee, EmployeeDto>();
                cfg.CreateMap<Owner, OwnerDto>();
                cfg.CreateMap<Reservation, ReservationDto>();
                cfg.CreateMap<Restaurant, RestaurantDto>();
                cfg.CreateMap<Table, TableDto>();
            })
            .CreateMapper();
    }
}
