using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ZaklepTo.Infrastucture.DTO.OnUpdate;

namespace ZaklepTo.Infrastucture.Validators
{
    public class PasswordChangeValidator : AbstractValidator<PasswordChange>
    {
        public PasswordChangeValidator()
        {
            var loginPattern = new Regex("^[a-zA-Z0-9]");

            var passwordSpecialChar = new Regex("[!@#$%^&*()_+}{\":?><,./; '[]|\\]");
            var passwordUpperCase = new Regex("[A-Z]");
            var passwordNumbers = new Regex("[0-9]");

            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage("Login can't be empty.")
                .Length(5, 20)
                .WithMessage("Login must be longer than 5 and shorter than 20 characters.")
                .Matches(loginPattern)
                .WithMessage("Login can't contain any special characters.");

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage("New password can't be empty.")
                .Length(8, 15)
                .WithMessage("New password must be longer than 5 and shorter than 15 characters.")
                .Matches(passwordSpecialChar)
                .WithMessage("New password must contain at least one special character.")
                .Matches(passwordUpperCase)
                .WithMessage("New password must contain at least one capital letter.")
                .Matches(passwordNumbers)
                .WithMessage("New password must contain at least one number.");
        }
    }
}
