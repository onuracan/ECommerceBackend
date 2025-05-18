using AutoMapper;
using ECommerceBackendSystem.API.Models.Orders.Request;
using ECommerceBackendSystem.Application.Abstractions.Dtos.Orders;

namespace ECommerceBackendSystem.API.Mappings;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<CreateOrderRequest, CreateOrderDto>()
            .ForMember(dest => dest.UserId, src => src.MapFrom(x => Guid.Parse(x.UserId)))
            .ForMember(dest => dest.ProductId, src => src.MapFrom(x => Guid.Parse(x.ProductId)));
    }
}
