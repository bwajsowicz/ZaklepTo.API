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

        public Owner(string login, string firstname, string lastname, string email,
        string phone, string password, string salt)
        {
            SetLogin(login);
            SetFirstName(firstname);
            SetLastName(lastname);
            SetEmail(email);
            SetPhone(phone);
            SetPassword(password);
            SetSalt(salt);
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
