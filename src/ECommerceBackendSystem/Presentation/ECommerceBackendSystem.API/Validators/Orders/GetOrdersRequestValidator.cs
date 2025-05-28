using ECommerceBackendSystem.API.Models.Orders.Request;
using FluentValidation;

namespace ECommerceBackendSystem.API.Validators.Orders;

public class GetTokenRequestValidator : AbstractValidator<GetOrdersRequest>
{
    public GetTokenRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("Enter the user id.");
    }
}
