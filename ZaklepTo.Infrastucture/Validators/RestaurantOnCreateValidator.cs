using FluentValidation;
using ZaklepTo.Infrastructure.DTO.OnCreate;

namespace ZaklepTo.Infrastructure.Validators
{
    public class RestaurantOnCreateValidator : AbstractValidator<RestaurantOnCreateDto>
    {
        public RestaurantOnCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode("Name can't be empty.")
                .MaximumLength(50)
                .WithErrorCode("Name can't be longer than 50 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description can't be empty.")
                .MaximumLength(200)
                .WithMessage("Description can't be longer than 200 characters.");

            RuleFor(x => x.Cuisine)
                .NotEmpty()
                .WithMessage("Cuisine can't be empty.")
                .MaximumLength(50)
                .WithMessage("Cuisine can't be longer than 50 characters");

            RuleFor(x => x.RepresentativePhotoUrl)
                .NotEmpty()
                .WithMessage("Url can't be empty.");

            RuleFor(x => x.Localization)
                .NotEmpty()
                .WithMessage("Localization can't be empty.")
                .MaximumLength(200)
                .WithMessage("Localization can't be longer than 200 characters.");

            RuleFor(x => x.Tables)
                .NotEmpty()
                .WithMessage("Every restaurant must contain set of tables.");
        }
    }
}
