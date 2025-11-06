using BankSystem.Domain.Enums;
using FluentValidation;
namespace BankSystem.Application.Features.Auth.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.Request.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .MaximumLength(100)
            .WithMessage("First name must not exceed 100 characters.")
            .MinimumLength(2)
            .WithMessage("First name must be at least 2 characters.")
            .Matches(@"^[a-zA-Z\s\-']+$")
            .WithMessage("First name can only contain letters, spaces, hyphens, and apostrophes.");

        RuleFor(x => x.Request.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .MaximumLength(100)
            .WithMessage("Last name must not exceed 100 characters.")
            .MinimumLength(2)
            .WithMessage("Last name must be at least 2 characters.")
            .Matches(@"^[a-zA-Z\s\-']+$")
            .WithMessage("Last name can only contain letters, spaces, hyphens, and apostrophes.");

        RuleFor(x => x.Request.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .MaximumLength(255)
            .WithMessage("Email must not exceed 255 characters.")
            .EmailAddress()
            .WithMessage("Email must be a valid email address.");

        RuleFor(x => x.Request.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .MaximumLength(20)
            .WithMessage("Phone number must not exceed 20 characters.")
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .WithMessage("Phone number must be in valid international format");

        RuleFor(x => x.Request.Address)
            .NotEmpty()
            .WithMessage("Address is required.")
            .MinimumLength(10)
            .WithMessage("Address must be at least 10 characters.")
            .MaximumLength(500)
            .WithMessage("Address must not exceed 500 characters.");

        RuleFor(x => x.Request.NationalId)
            .NotEmpty()
            .WithMessage("National ID is required.")
            .MaximumLength(50)
            .WithMessage("National ID must not exceed 50 characters.")
            .Matches(@"^[A-Z0-9\-]+$")
            .WithMessage("National ID can only contain uppercase letters, numbers, and hyphens.");

        RuleFor(x => x.Request.DateOfBirth)
            .NotEmpty()
            .WithMessage("Date of birth is required.")
            .Must(BeValidDateOfBirth)
            .WithMessage("Date of birth must be a valid past date.")
            .Must(BeAtLeast18YearsOld)
            .WithMessage("Customer must be at least 18 years old.")
            .Must(BeReasonableAge)
            .WithMessage("Age must be less than 120 years.");

        RuleFor(x => x.Request.Type)
            .NotEmpty()
            .WithMessage("Customer type is required.")
            .Must(type => Enum.GetNames(typeof(CustomerType)).Contains(type))
            .WithMessage($"Invalid customer type. Valid values are: {string.Join(", ", Enum.GetNames(typeof(CustomerType)))}");
    }

    private bool BeValidDateOfBirth(DateTime dateOfBirth)
    {
        return dateOfBirth < DateTime.UtcNow.Date;
    }

    private bool BeAtLeast18YearsOld(DateTime dateOfBirth)
    {
        var today = DateTime.UtcNow.Date;
        var age = today.Year - dateOfBirth.Year;

        if (dateOfBirth.Date > today.AddYears(-age))
        {
            age--;
        }

        return age >= 18;
    }

    private bool BeReasonableAge(DateTime dateOfBirth)
    {
        var age = DateTime.UtcNow.Year - dateOfBirth.Year;
        return age <= 120;
    }
}