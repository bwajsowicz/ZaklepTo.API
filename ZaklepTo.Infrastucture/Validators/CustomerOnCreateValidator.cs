using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using FluentValidation;
using ZaklepTo.Infrastucture.DTO;

namespace ZaklepTo.Infrastucture.Validators
{
    public class CustomerOnCreateValidator : AbstractValidator<CustomerOnCreateDTO>
    {
        // TODO move properties to constructor?

        private readonly Regex loginPattern = new Regex("^[a-zA-Z0-9]");

        private readonly Regex passwordSpecialChar = new Regex("[!@#$%^&*()_+}{\":?><,./; '[]|\\]");
        private readonly Regex passwordUpperCase = new Regex("[A-Z]");
        private readonly Regex passwordNumbers = new Regex("[0-9]");

        private readonly Regex phonePattern = new Regex("^[0-9]");

        public CustomerOnCreateValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage("Login can't be empty.")
                .Length(5, 20)
                .WithMessage("Login must be longer than 5 and shorter than 20 characters.")
                .Matches(loginPattern)
                .WithMessage("Login can't contain any special characters.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password can't be empty.")
                .Length(8, 15)
                .WithMessage("Password must be longer than 5 and shorter than 15 characters.")
                .Matches(passwordSpecialChar)
                .WithMessage("Password must containt at least one special character.")
                .Matches(passwordUpperCase)
                .WithMessage("Password must containt at leas one capital letter.")
                .Matches(passwordNumbers)
                .WithMessage("Password must containt at leas one number.");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("FirstName can't be empty")
                .MaximumLength(30)
                .WithMessage("FirstName can't be longer than 30 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("LastName can't be empty")
                .MaximumLength(30)
                .WithMessage("LastName can't be longer than 30 characters.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email can't be empty")
                .MaximumLength(35)
                .WithMessage("Email can't be longer than 35 characters.")
                .EmailAddress()
                .WithMessage("A valid email adress is required.");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("Phone can't be empty")
                .MaximumLength(25)
                .WithMessage("Phone cant't be longer than 25 characters.")
                .Matches(phonePattern)
                .WithMessage("Phone must consists of numbers only.");
        }
    }
}
