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

        protected Table()
        {
        }

        public Table(int numberOfSeats, (int x, int y) coordinates)
        {
            Id = Guid.NewGuid();
            NumberOfSeats = numberOfSeats;
            Coordinates = coordinates;
        }
    }
}
