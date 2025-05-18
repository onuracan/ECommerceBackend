using ECommerceBackendSystem.Application.Abstractions.Services.Base;
using ECommerceBackendSystem.Domain.Entities.Orders;

namespace ECommerceBackendSystem.Application.Abstractions.Services.OrderService;

public interface IOrderCommandService : IGenericCommandService<Order>
{
}
