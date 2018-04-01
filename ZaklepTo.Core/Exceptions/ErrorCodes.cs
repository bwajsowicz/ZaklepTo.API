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
        public static string CustomerAlreadyExists => "user_already_exists";
        public static string CustomerNotFound => "user_not_found";
    }
}
