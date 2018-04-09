using System;

namespace ZaklepTo.Infrastructure.DTO.OnCreate
{
    public class ReservationOnCreateDTO
    {
        public RestaurantOnCreateDTO Restaurant { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public TableDTO Table { get; set; }
        public CustomerOnCreateDTO Customer { get; set; }
    }
}
