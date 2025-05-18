using AutoMapper;
using ECommerceBackendSystem.API.Mappings;
using ECommerceBackendSystem.Application.Abstractions.Dtos.Orders;
using ECommerceBackendSystem.Tests;

namespace ECommerceBackendSystem.API.Tests.Mappings;

public class OrderMappingProfileTests : TestBase
{
    private readonly IMapper _mapper;

    public OrderMappingProfileTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<OrderProfile>();
        });
        _mapper = config.CreateMapper();
    }

    [Fact]
    public void CreateOrderRequest_To_OrderDto_Mapping_IsValid()
    {
        var order = CreateACreateOrderRequest();

        var dto = _mapper.Map<CreateOrderDto>(order);

        Assert.Equal(order.ProductId, dto.ProductId.ToString());
        Assert.Equal(order.Quantity, dto.Quantity);
        Assert.Equal(order.PaymentMethod, dto.PaymentMethod);
        Assert.Equal(order.UserId, dto.UserId.ToString());
    }
}
