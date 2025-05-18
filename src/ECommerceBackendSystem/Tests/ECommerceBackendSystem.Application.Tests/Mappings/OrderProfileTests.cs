using AutoMapper;
using ECommerceBackendSystem.Application.Abstractions.Dtos.Orders;
using ECommerceBackendSystem.Domain.Entities.Orders;
using ECommerceBackendSystem.Tests;

namespace ECommerceBackendSystem.Application.Tests.Mappings;

public class OrderMappingProfileTests:TestBase
{
    private readonly IMapper _mapper;

    public OrderMappingProfileTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ECommerceBackendSystem.Application.Mappings.OrderProfile>();
        });
        _mapper = config.CreateMapper();
    }

    [Fact]
    public void Order_To_OrderDto_Mapping_IsValid()
    {
        var order = CreateAnOrder();

        var dto = _mapper.Map<OrderDto>(order);

        Assert.Equal(order.ProductId, dto.ProductId);
        Assert.Equal(order.Quantity, dto.Quantity);
        Assert.Equal(order.PaymentMethod, dto.PaymentMethod);
        Assert.Equal(order.UserId, dto.UserId);
    }

    [Fact]
    public void OrderDto_To_Order_Mapping_IsValid()
    {
        var dto = CreateAnOrderDto();

        var order = _mapper.Map<Order>(dto);

        Assert.Equal(dto.ProductId, order.ProductId);
        Assert.Equal(dto.Quantity, order.Quantity);
        Assert.Equal(dto.PaymentMethod, order.PaymentMethod);
        Assert.Equal(dto.UserId, order.UserId);
    }

    [Fact]
    public void CreateOrderDto_To_Order_Mapping_IsValid()
    {
        var createOrderDto = CreateACreateOrderDto();

        var order = _mapper.Map<Order>(createOrderDto);

        Assert.Equal(order.ProductId, createOrderDto.ProductId);
        Assert.Equal(order.Quantity, createOrderDto.Quantity);
        Assert.Equal(order.PaymentMethod, createOrderDto.PaymentMethod);
        Assert.Equal(order.UserId, createOrderDto.UserId);
    }
}
