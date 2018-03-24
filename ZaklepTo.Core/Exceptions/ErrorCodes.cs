using System;
using System.Collections.Generic;
using System.Text;

namespace ZaklepTo.Core.Exceptions
{
    public static class ErrorCodes
    {
        public static string InvalidLogin => "invalid_login";
        public static string InvalidFirstName => "invalid_firstname";
        public static string InvalidLastName => "invalid_lastname";
        public static string InvalidEmail => "invalid_email";
        public static string InvalidPhone => "invalid_phone";
        public static string InvalidPassword => "invalid_password";
        public static string InvalidSalt => "invalid_salt";
        public static string InvalidRestaurantName => "invalid_restaurantname";
        public static string InvalidDescription => "invalid_description";
        public static string InvalidCuisine => "invalid_cuisine";
        public static string InvalidLocalization => "invalid_localization";
        public static string InvalidNumberOfSeats => "invalid_numberofseats";
        public static string InvalidCoordinates => "invalid_coordinates";
        public static string InvalidId => "invalid_id";
        public static string InvalidRestaurant => "invalid_restaurant";
    }
}
