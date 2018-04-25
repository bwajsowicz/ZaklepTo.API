using System;
using ZaklepTo.Core.Domain;

namespace ZaklepTo.Infrastructure.DTO.OnCreate
{
    public class ReservationOnCreateDto
    {
        public RestaurantDto Restaurant { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public TableDto Table { get; set; }
        public CustomerDto Customer { get; set; }
    }
}
