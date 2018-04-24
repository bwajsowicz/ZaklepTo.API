using System;

namespace ZaklepTo.Core.Domain
{
    public class Table
    {
        public Guid Id { get; set; }
        public int NumberOfSeats { get; set; }
        public (int x, int y) Coordinates { get; set; }

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
