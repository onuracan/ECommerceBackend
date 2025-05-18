using ECommerceBackendSystem.API.Models.Tokens;
using FluentValidation;

namespace ECommerceBackendSystem.API.Validators.Tokens;

public class GetTokenRequestValidator : AbstractValidator<GetTokenRequest>
{
    public GetTokenRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("Enter the user id.")
                              .Must(id => Guid.TryParse(id, out _)).WithMessage("User ID must be a valid GUID.");
    }
}
