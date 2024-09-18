namespace Template.API.Validations
{
    using api.Domain.Entities;
    using FluentValidation;

    public class UserValidator : AbstractValidator<User> 
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required")
            .MinimumLength(6).WithMessage("User name must be at least 6 characters long");

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required")
                .MinimumLength(2).WithMessage("First Name must be at least 2 characters long");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required")
                .MinimumLength(2).WithMessage("Last Name must be at least 2 characters long");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Address is required").
                EmailAddress().WithMessage("Provide valid Email Address");

            RuleFor(x => x.Role).NotEmpty().WithMessage("Role is required").Must(ValidateRole)
                .WithMessage("Role must be Admin, Student, Instructor");

            RuleFor(x => x.PasswordHash).NotEmpty().WithMessage("Password Field is required").
                MinimumLength(8).WithMessage("Password should be at least 8 characters long").MaximumLength(25)
                .WithMessage("Password should be at most 25 characters long").Must(ValidatePassword).WithMessage("Password must contain one upper, lower, digit and special characters");
        }

        public bool ValidatePassword(string password)
        {
            bool isValid = false;

            if (password.Any(Char.IsUpper) && password.Any(Char.IsLower) && password.Any(Char.IsDigit) && password.Any(ch => !char.IsLetterOrDigit(ch)) && !password.Any(Char.IsWhiteSpace))
            {
                isValid = true;
            }

            return isValid;
        }

        public bool ValidateRole(string role)
        {
            bool ret = false;
            if(role.ToLower() == "admin" || role.ToLower() == "student" || role.ToLower() == "instructor")
            {
                ret = true;
            }
            return ret;
        }

    }

}
