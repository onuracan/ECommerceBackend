using ECommerceBackendSystem.Application.Constants;

namespace ECommerceBackendSystem.Application.Tests.Constants;

public class ProccessMessageConstantsTests
{
    [Fact]
    public void OrderCompletedConstant_ShouldBe_Correct()
    {
        Assert.Equal("Order entry is completed.", ProccessMessageConstants.ORDER_COMPLETED);
    }
}
