using AutoMapper;
using ECommerceBackendSystem.Application.Abstractions.Dtos.Orders;
using ECommerceBackendSystem.Application.Abstractions.Dtos.ServiceResponse;
using ECommerceBackendSystem.Application.Abstractions.Services.Base;
using ECommerceBackendSystem.Application.Abstractions.Services.OrderService;
using ECommerceBackendSystem.Application.Abstractions.Services.OrderService.Strategy;
using ECommerceBackendSystem.Application.Constants;
using ECommerceBackendSystem.Domain.Entities.Orders;
using ECommerceBackendSystem.Domain.Repositories;
using ECommerceBackendSystem.Domain.Shared.Enums;

namespace ECommerceBackendSystem.Application.OrderService.Strategy;

public class OrderBankTransferStrategy(IGenericRepository<Order> repository,
                                       IServiceResponseHelper serviceResponseHelper,
                                       IOrderProviderService orderProviderService,
                                       IMapper mapper) : IOrderStrategy
{
    private readonly IGenericRepository<Order> _repository = repository;
    private readonly IServiceResponseHelper _serviceResponseHelper = serviceResponseHelper;
    private readonly IOrderProviderService _orderProviderService = orderProviderService;
    private readonly IMapper _mapper = mapper;

    public string PaymentMethod => PaymentMethodConstants.BANK_TRANSFER;

    public async Task<ServiceResponse> CreateOrderAsync(CreateOrderDto createOrder)
    {
        var newOrder = this._mapper.Map<Order>(createOrder);
        newOrder.IsActive = (int)ActiveFlag.Active;

        await this._repository.AddAsync(newOrder).ConfigureAwait(false);

        await this._orderProviderService.ExecuteRabbitMqAndRedisServicesAsync(newOrder).ConfigureAwait(false);

        return this._serviceResponseHelper.Success(ProccessMessageConstants.ORDER_COMPLETED);
    }
}
