using ECommerceBackendSystem.Application.Constants;

namespace ECommerceBackendSystem.Application.Tests.Constants;

public class PaymentMethodConstantsTests
{
    [Fact]
    public void CreditCardConstant_ShouldBe_Correct()
    {
        Assert.Equal("CreditCard", PaymentMethodConstants.CREDIT_CARD);
    }

    [Fact]
    public void BankTransferConstant_ShouldBe_Correct()
    {
        Assert.Equal("BankTransfer", PaymentMethodConstants.BANK_TRANSFER);
    }
}
