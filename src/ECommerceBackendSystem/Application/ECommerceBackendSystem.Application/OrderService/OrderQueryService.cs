using ECommerceBackendSystem.Application.Abstractions.Caching;
using ECommerceBackendSystem.Application.Abstractions.Dtos.Orders;
using ECommerceBackendSystem.Application.Abstractions.Dtos.ServiceResponse;
using ECommerceBackendSystem.Application.Abstractions.Services.Base;
using ECommerceBackendSystem.Application.Abstractions.Services.OrderService;
using ECommerceBackendSystem.Application.Base;
using ECommerceBackendSystem.Domain.Entities.Orders;
using ECommerceBackendSystem.Domain.Repositories;
using System.Net;

namespace ECommerceBackendSystem.Application.OrderService;

public class OrderQueryService(IGenericRepository<Order> repository,
                               IServiceResponseHelper serviceResponseHelper,
                               IRedisCacheService redisCacheService) : GenericQueryService<Order>(repository), IOrderQueryService
{
    private readonly IGenericRepository<Order> _repository = repository;
    private readonly IServiceResponseHelper _serviceResponseHelper = serviceResponseHelper;
    private readonly IRedisCacheService _redisCacheService = redisCacheService;

    public async Task<ServiceResponse<IEnumerable<OrderDto>>> GetOrdersByUserIdAsync(Guid userId)
    {
        if (this._redisCacheService.KeyExists(userId.ToString()))
        {
            var cacheOrders = await this._redisCacheService.GetAsync<IEnumerable<OrderDto>>(userId.ToString()).ConfigureAwait(false);

            return this._serviceResponseHelper.Success<IEnumerable<OrderDto>>(cacheOrders);
        }

        var query = this._repository.GetQueryable(x => x.UserId == userId);

        if (!query.Any())
            return this._serviceResponseHelper.Fail<IEnumerable<OrderDto>>("Kullanıcıya ait sipariş(ler) bulunamadı.", HttpStatusCode.NoContent);

        var orders = query.Select(x => new OrderDto()
        {
            Id = x.Id,
            ProductId = x.ProductId,
            Quantity = x.Quantity,
            PaymentMethod = x.PaymentMethod,
            UserId = x.UserId,
            IsActive = x.IsActive
        }).ToList();

        await this._redisCacheService.SetAsync<IEnumerable<OrderDto>>(userId.ToString(), orders, TimeSpan.FromMinutes(2)).ConfigureAwait(false);

        return this._serviceResponseHelper.Success<IEnumerable<OrderDto>>(orders);
    }
}
