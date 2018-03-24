using System;
using System.Collections.Generic;
using System.Text;
using ZaklepTo.Core.Exceptions;
using ZaklepTo.Core.Extensions;

namespace ZaklepTo.Core.Domain
{
    public class Owner : User
    {
        public Restaurant Restaurant { get; private set; }

        public Owner(Restaurant restaurant)
        {
            SetRestaurant(restaurant);
            CreatedAt = DateTime.UtcNow;
        }

        public void SetRestaurant(Restaurant restaurant)
        {
            if (restaurant.ToString().Empty())
            {
                throw new DomainException(ErrorCodes.InvalidRestaurant, "Restaurant can't be null or empty.");
            }
            if (Restaurant == restaurant)
            {
                return;
            }
            Restaurant = restaurant;
        }

    }
}
