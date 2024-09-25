using api.Domain.Entities;
using FluentValidation;

namespace api.Application.Validators
{
    public class UserLoginValidator : AbstractValidator<UserLogin>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.userName).NotEmpty().WithMessage("User name is required")
            .MinimumLength(6).WithMessage("User name must be at least 6 characters long");

            
            RuleFor(x => x.password).NotEmpty().WithMessage("Password Field is required").
                MinimumLength(8).WithMessage("Password should be at least 8 characters long").MaximumLength(25)
                .WithMessage("Password should be at most 25 characters long").Must(ValidatePassword).WithMessage("Password must contain one upper, lower, digit and special characters");
        }

        // Password should contain atleast one Uppercase, one lowerCase, one digit, and does not contain whitespace.
        public bool ValidatePassword(string password)
        {
            bool isValid = false;

            if (!string.IsNullOrEmpty(password) && password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit) && password.Any(ch => !char.IsLetterOrDigit(ch)) && !password.Any(char.IsWhiteSpace))
            {
                isValid = true;
            }

            return isValid;
        }

    }
}
