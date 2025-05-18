using AutoMapper;
using ECommerceBackendSystem.Application.Abstractions.Dtos.Events;
using ECommerceBackendSystem.Application.Abstractions.Dtos.Orders;
using ECommerceBackendSystem.Domain.Entities.Orders;

namespace ECommerceBackendSystem.Application.Mappings;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<CreateOrderDto, Order>();
        CreateMap<Order, OrderCreatedEvent>();
        CreateMap<Order, OrderDto>().ReverseMap();
    }
}
