using System;

namespace ZaklepTo.Infrastructure.DTO
{
    public class TableDTO
    {
        public Guid Id { get; set; }
        public int NumberOfSeats { get; set; }
        public (int x, int y) Coordinates { get; set; }
    }
}
