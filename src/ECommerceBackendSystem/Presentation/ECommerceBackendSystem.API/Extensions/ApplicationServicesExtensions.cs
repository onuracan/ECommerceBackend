using ECommerceBackendSystem.Application.Abstractions.Publisher;
using ECommerceBackendSystem.Application.Abstractions.Services.Base;
using ECommerceBackendSystem.Application.Abstractions.Services.OrderService;
using ECommerceBackendSystem.Application.Abstractions.Services.OrderService.Factory;
using ECommerceBackendSystem.Application.Abstractions.Services.OrderService.Strategy;
using ECommerceBackendSystem.Application.Abstractions.Services.TokenService;
using ECommerceBackendSystem.Application.Base;
using ECommerceBackendSystem.Application.OrderService;
using ECommerceBackendSystem.Application.OrderService.Factory;
using ECommerceBackendSystem.Application.OrderService.Strategy;
using ECommerceBackendSystem.Application.TokenService;
using ECommerceBackendSystem.Infrastructure.Messaging;

namespace ECommerceBackendSystem.API.Extensions;

public static class ApplicationServicesExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IServiceResponseHelper, ServiceResponseHelper>();
        services.AddScoped<IOrderPlacedPublisher, OrderPlacedPublisher>();
        services.AddScoped<IOrderQueryService, OrderQueryService>();
        services.AddScoped<IOrderStrategy, OrderCreditCardStrategy>();
        services.AddScoped<IOrderStrategy, OrderBankTransferStrategy>();
        services.AddScoped<IOrderStrategyFactory, OrderStrategyFactory>();
        services.AddScoped<IOrderProviderService, OrderProviderService>();
        services.AddScoped<ITokenService, TokenService>();
    }
}
