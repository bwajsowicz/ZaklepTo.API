using System;
using System.Collections.Generic;
using System.Text;

namespace ZaklepTo.Core.Domain
{
    public abstract class User
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime CreatedAt { get; set; }

        protected User()
        {
        }

        public User(string login, string firstname, string lastname, string email,
            string phone, string password, string salt)
        {
            Login = login;
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            Phone = phone;
            Password = password;
            Salt = salt;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
