using ECommerceBackendSystem.API.Models.Orders.Request;
using ECommerceBackendSystem.Application.Constants;
using FluentValidation;

namespace ECommerceBackendSystem.API.Validators.Orders;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("Enter the user id.")
                              .Must(id => Guid.TryParse(id, out _)).WithMessage("User ID must be a valid GUID.");
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Enter the product id.")
                                 .Must(id => Guid.TryParse(id, out _)).WithMessage("Product ID must be a valid GUID.");
        RuleFor(x => x).Must(x => x.UserId != x.ProductId).WithMessage("User ID and Product ID cannot be the same.");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Enter the quantity.");
        RuleFor(x => x.PaymentMethod).Must(x => x == PaymentMethodConstants.CREDIT_CARD || x == PaymentMethodConstants.BANK_TRANSFER)
            .WithMessage($"Payment type must be of the {PaymentMethodConstants.CREDIT_CARD} or {PaymentMethodConstants.BANK_TRANSFER} types.");
    }
}
