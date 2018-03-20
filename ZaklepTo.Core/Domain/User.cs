using System;
using System.Collections.Generic;
using System.Text;

namespace ZaklepTo.Core.Domain
{
    public abstract class User
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; protected set; }
        public string Phone { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        //niektóre biblioteki mogą wymagać konstruktora bezparametrowego
        protected User()
        {
        }

        public User(string firstname, string lastname, string email,
            string phone, string password, string salt)
        {
            //trzeba zaimplenetować walidację 
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
