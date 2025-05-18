using ECommerceBackendSystem.Application.Abstractions.Dtos.Orders;
using ECommerceBackendSystem.Application.Abstractions.Dtos.ServiceResponse;
using ECommerceBackendSystem.Application.Abstractions.Services.Base;
using ECommerceBackendSystem.Domain.Entities.Orders;

namespace ECommerceBackendSystem.Application.Abstractions.Services.OrderService;

public interface IOrderQueryService : IGenericQueryService<Order>
{
    Task<ServiceResponse<IEnumerable<OrderDto>>> GetOrdersByUserIdAsync(Guid userId);
}
