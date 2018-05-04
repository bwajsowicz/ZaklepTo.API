namespace ZaklepTo.Core.Domain
{
    public class Customer : User
    {
        protected Customer() : base()
        {
        }

        public Customer(string login, string firstname, string lastname, string email,
            string phone, string password, string salt)
            : base(login, firstname, lastname, email, phone, password, salt)
        {
        }
    }
}
