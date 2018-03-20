using System;
using System.Collections.Generic;
using System.Text;

namespace ZaklepTo.Core.Domain
{
    public class Table
    {
        public Guid Id { get; private set; }
        public int NumberOfSeats { get; private set; }
        public (int x, int y) Coordinates { get; private set; }

        public Table(Guid id, int numberOfSeats, (int x, int y) coordinates)
        {
            Id = id;
            NumberOfSeats = numberOfSeats;
            Coordinates = coordinates;
        }
    }
}
