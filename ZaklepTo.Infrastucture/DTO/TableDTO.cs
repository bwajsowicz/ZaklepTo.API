using System;
using System.Collections.Generic;
using System.Text;

namespace ZaklepTo.Infrastucture.DTO
{
    public class TableDTO
    {
        public Guid Id { get; set; }
        public int NumberOfSeats { get; set; }
        public (int x, int y) Coordinates { get; set; }
    }
}
