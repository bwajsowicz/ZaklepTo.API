using System;

namespace ZaklepTo.Infrastructure.DTO.OnUpdate
{
    public class ReservationOnUpdateDTO
    {
        public Guid Id { get; set; }
        public RestaurantDTO Restaurant { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public TableDTO Table { get; set; }
        public CustomerDTO Customer { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
