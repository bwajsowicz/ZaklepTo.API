using System;
using System.Collections.Generic;
using System.Text;
using ZaklepTo.Core.Exceptions;
using System.Text.RegularExpressions;

namespace ZaklepTo.Core.Domain
{
    public abstract class User
    {
        public string Login { get; private set; }
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

        public User(string login, string firstname, string lastname, string email,
            string phone, string password, string salt)
        {
            //trzeba zaimplenetować walidację 
            Login = login;
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            Phone = phone;
            Password = password;
            Salt = salt;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetLogin(string login)
        {
            Regex LoginPattern = new Regex("[A-Za-z0-9]{5,20}");
            if (string.IsNullOrWhiteSpace(login) || LoginPattern.IsMatch(login))
            {
               throw new DomainException(ErrorCodes.InvalidLogin, "Login can't contain special characters, white spaces and be less than 5 and longer than 20 characters.");
            }
        }
    }
}

