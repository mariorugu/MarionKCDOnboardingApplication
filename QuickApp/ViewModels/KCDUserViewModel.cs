using FluentValidation;

namespace QuickApp.ViewModels;

public class KCDUserViewModel
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get;  set; } // to encrypt
    public string Country { get; set; }
    public string Company { get; set; }
    public string Miscellaneous  { get; set; }
}

public class KCDUserViewModelValidator : AbstractValidator<KCDUserViewModel>
{
    public KCDUserViewModelValidator()
    {
        RuleFor(register => register.FirstName).NotEmpty().WithMessage("User first name cannot be empty").Must(name => name.Length <= 50)
            .WithMessage("User first name should have a length less than 50");
        RuleFor(register => register.LastName).NotEmpty().WithMessage("User middle name cannot be empty").Must(name => name.Length <= 50).WithMessage(
            "User first name should have a length less than 50");
        RuleFor(register => register.Email).NotEmpty().WithMessage("User email cannot be empty").EmailAddress().WithMessage("A valid email is required");
        RuleFor(register => register.Password).NotEmpty().WithMessage("User password cannot be empty").Length(8, 50).WithMessage("User password must be between 8 and 50 characters");
        RuleFor(register => register.Country).Must(name => name.Length <= 50).NotEmpty()
            .WithMessage("User country cannot be empty");
    }
}