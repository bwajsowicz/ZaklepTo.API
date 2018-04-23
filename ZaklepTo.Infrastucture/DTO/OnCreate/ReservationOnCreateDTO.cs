using System;
using ZaklepTo.Core.Domain;

namespace ZaklepTo.Infrastructure.DTO.OnCreate
{
    public class ReservationOnCreateDTO
    {
        public Restaurant Restaurant { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public TableDTO Table { get; set; }
        public Customer Customer { get; set; }
    }
}
