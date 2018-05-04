namespace ZaklepTo.Core.Domain
{
    public class Owner : User
    {
        public Restaurant Restaurant { get; set; }

        protected Owner() : base()
        {
        }

        public Owner(string login, string firstname, string lastname, string email,
            string phone, string password, string salt, Restaurant restaurant)
            : base(login, firstname, lastname, email, phone, password, salt)
        {
            Restaurant = restaurant;
        }
    }
}
