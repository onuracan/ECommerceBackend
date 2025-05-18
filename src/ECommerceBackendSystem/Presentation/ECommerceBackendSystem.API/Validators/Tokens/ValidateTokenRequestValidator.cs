using ECommerceBackendSystem.API.Models.Tokens;
using FluentValidation;

namespace ECommerceBackendSystem.API.Validators.Tokens;

public class ValidateTokenRequestValidator : AbstractValidator<ValidateTokenRequest>
{
    public ValidateTokenRequestValidator()
    {
        RuleFor(x => x.Token).NotEmpty().WithMessage("Enter the token.");
    }
}
