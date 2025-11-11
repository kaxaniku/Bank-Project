using BankSystem.Domain.Enums;
using FluentValidation;

namespace BankSystem.Application.Features.Auth.Commands.CreateAccount;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.Request.CustomerId)
            .GreaterThan(0)
            .WithMessage("Customer ID must be greater than 0.");

        RuleFor(x => x.Request.Currency)
            .NotEmpty()
            .WithMessage("Currency is required.")
            .Length(3)
            .WithMessage("Currency must be a 3-letter ISO code.")
            .Must(currency => Enum.GetNames(typeof(Currency)).Contains(currency))
            .WithMessage($"Currency must be one of: {string.Join(", ", Enum.GetNames(typeof(Currency)))}.");

        RuleFor(x => x.Request.Type)
            .NotEmpty()
            .WithMessage("Account type is required.")
            .Must(type => Enum.GetNames(typeof(AccountType)).Contains(type))
            .WithMessage($"Invalid account type. Valid values are: {string.Join(", ", Enum.GetNames(typeof(AccountType)))}");
    }
}