using System;
using System.Collections.Generic;
using System.Text;
using ZaklepTo.Core.Exceptions;
using ZaklepTo.Core.Extensions;
using System.Text.RegularExpressions;

namespace ZaklepTo.Core.Domain
{
    public abstract class User
    {
        public string Login { get; protected set; }
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
            SetLogin(login);
            SetFirstName(firstname);
            SetLastName(lastname);
            SetEmail(email);
            SetPhone(phone);
            SetPassword(password);
            SetSalt(salt);
            CreatedAt = DateTime.UtcNow;
        }

        public void SetLogin(string login)
        {
            Regex LoginPattern = new Regex("[^a-zA-Z0-9]");

            if (login.Empty())
            {
                throw new DomainException(ErrorCodes.InvalidLogin, "Login can't be empty.");
            }
            else if (LoginPattern.IsMatch(login) || login.Length > 20 || login.Length < 5)
            {
                throw new DomainException(ErrorCodes.InvalidLogin, "Login can't contain special characters and be less than 5 and longest than 20 characters.");
            }

            if (Login == login)
            {
                return;
            }

            Login = login;
        }

        public void SetFirstName(string firstname)
        {
            Regex FirstNamePattern = new Regex("[^a-zA-Z]");

            if (firstname.Empty())
            {
                throw new DomainException(ErrorCodes.InvalidFirstName, "First name can't be empty.");
            }
            else if (FirstNamePattern.IsMatch(firstname) || firstname.Length > 30 )
            {
                throw new DomainException(ErrorCodes.InvalidFirstName, "First name can't contain special characters and be longest than 20 characters.");
            }

            if (FirstName == firstname)
            {
                return;
            }

            Login = firstname;
        }

        public void SetLastName(string lastname)
        {
            Regex LastNamePattern = new Regex("[^a-zA-Z]");

            if (lastname.Empty())
            {
                throw new DomainException(ErrorCodes.InvalidLastName, "Last name can't be empty.");
            }
            else if (LastNamePattern.IsMatch(lastname) || lastname.Length > 30)
            {
                throw new DomainException(ErrorCodes.InvalidLastName, "Last name can't contain special characters and be longest than 30 characters.");
            }

            if (LastName == lastname)
            {
                return;
            }

            LastName = lastname;
        }

        public void SetEmail(string email)
        {
            Regex EmailPattern = new Regex("[@]");

            if (email.Empty())
            {
                throw new DomainException(ErrorCodes.InvalidEmail, "Email name can't be empty.");
            }
            else if (!EmailPattern.IsMatch(email) || email.Length > 25)
            {
                throw new DomainException(ErrorCodes.InvalidEmail, "Email must contain @ and be less than 25.");
            }

            if (Email == email)
            {
                return;
            }

            Email = email;
        }

        public void SetPhone(string phone)
        {
            Regex PhonePattern = new Regex("[^0-9]");

            if (phone.Empty())
            {
                throw new DomainException(ErrorCodes.InvalidPhone, "Phone can't be empty.");
            }
            else if (PhonePattern.IsMatch(phone) || phone.Length > 25)
            {
                throw new DomainException(ErrorCodes.InvalidPhone, "Phone must contain only numbers and can't be longest than 25 characters.");
            }

            if (Phone == phone)
            {
                return;
            }

            Phone = phone;
        }

        public void SetPassword(string password)
        {
            Regex SpecialChar = new Regex("[!@#$%^&*()_+}{\":?><,./; '[]|\\]");
            Regex LettersLowercas = new Regex("[a-z]");
            Regex LettersUppercas = new Regex("[A-Z]");
            Regex Numbers = new Regex("[0-9]");

            if (password.Empty())
            {
                throw new DomainException(ErrorCodes.InvalidPassword, "Password name can't be empty.");
            }
            else if(password.Length > 15 || password.Length < 8)
            {
                throw new DomainException(ErrorCodes.InvalidPassword, "Password must be longest than 8 and less than 15 characters");
            }
            else if (!SpecialChar.IsMatch(password) || !LettersLowercas.IsMatch(password) || !LettersUppercas.IsMatch(password) || !Numbers.IsMatch(password))
            {
                throw new DomainException(ErrorCodes.InvalidPassword, "Password must contain at leas one special character, number and capital letter.");
            }

            if (Password == password)
            {
                return;
            }

            Password = password;
        }

        public void SetSalt(string salt)
        {
            if(salt.Empty())
            {
                throw new DomainException(ErrorCodes.InvalidSalt, "Salt can't be null or empty.");
            }
            Salt = salt;
        }
    }
}

