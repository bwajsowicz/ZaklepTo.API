using System;

namespace ZaklepTo.Infrastructure.DTO.OnUpdate
{
    public class ReservationOnUpdateDto
    {
        public RestaurantDto Restaurant { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public TableDto Table { get; set; }
        public CustomerDto Customer { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsActive { get; set; }
    }
}
