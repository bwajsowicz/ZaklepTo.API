using System;
using System.Collections.Generic;
using System.Text;
using ZaklepTo.Core.Extensions;
using ZaklepTo.Core.Exceptions;

namespace ZaklepTo.Core.Domain
{
    public class Table
    {
        public Guid Id { get; private set; }
        public int NumberOfSeats { get; private set; }
        public (int x, int y) Coordinates { get; private set; }

        Table((int x, int y) coordinates, int numberofseats, Guid id)
        {
            SetCoordinates(coordinates);
            SetNumberOfSeats(numberofseats);
            SetId(id);
        }

        public void SetNumberOfSeats(int numberofseats)
        {

            if(numberofseats.ToString().Empty())
            {
                throw new DomainException(ErrorCodes.InvalidNumberOfSeats, "Number of seats can't be empty.");
            }

            if(numberofseats <= 0)
            {
                throw new DomainException(ErrorCodes.InvalidNumberOfSeats, "Number of seats must be greater than 0.");
            }

            if(numberofseats == NumberOfSeats)
            {
                return;
            }

            NumberOfSeats = numberofseats;
        }

        public void SetCoordinates((int x, int y) coordinates)
        {
            if (coordinates.x.ToString().Empty() || coordinates.y.ToString().Empty())
            {
                throw new DomainException(ErrorCodes.InvalidCoordinates, "Coordinates can't be empty.");
            }

            if(Coordinates.x == coordinates.x && Coordinates.y == coordinates.y)
            {
                return;
            }

            Coordinates = coordinates;
        }

        public void SetId(Guid id)
        {
            if (id.ToString().Empty())
            {
                throw new DomainException(ErrorCodes.InvalidId,"Id can't be empty.");
            }

            if(Id == id)
            {
                return;
            }

            Id = id;
        }
    }
}
