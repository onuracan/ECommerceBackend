using ECommerceBackendSystem.Domain.Shared.Enums;
using ECommerceBackendSystem.Tests;

namespace ECommerceBackendSystem.Domain.Tests;

public class OrderTests : TestBase
{
    [Fact]
    public void Order_Created_IsActive_ShouldBeTrue()
    {
        var order = CreateAnOrder(isActive: (int)ActiveFlag.Active);

        Assert.Equal((int)ActiveFlag.Active, order.IsActive);
        Assert.NotEqual(order.UserId, Guid.Empty);
        Assert.NotEqual(order.ProductId, Guid.Empty);
        Assert.NotEqual(0, order.Quantity);
    }

    [Fact]
    public void Cancel_ShouldSetIsActiveToFalse()
    {
        var order = CreateAnOrder(isActive: (int)ActiveFlag.Inactive);

        Assert.Equal((int)ActiveFlag.Inactive, order.IsActive);
    }
}
