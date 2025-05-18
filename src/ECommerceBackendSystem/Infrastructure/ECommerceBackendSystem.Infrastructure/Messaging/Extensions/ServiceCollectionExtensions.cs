using ECommerceBackendSystem.Infrastructure.Messaging.Observer;
using ECommerceBackendSystem.Infrastructure.Messaging.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ECommerceBackendSystem.Infrastructure.Messaging.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqOptions>(x =>
        {
            x.HostName = configuration["RabbitMq:HostName"];
            x.UserName = configuration["RabbitMq:UserName"];
            x.Password = configuration["RabbitMq:Password"];
            x.QueueName = configuration["RabbitMq:QueueName"];
        });

        services.AddSingleton<OrderPlacedObserver>();

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                var observer = context.GetRequiredService<OrderPlacedObserver>();
                cfg.ConnectSendObserver(observer);

                var options = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;
                cfg.Host(options.HostName, "/", h =>
                {
                    h.Username(options.UserName);
                    h.Password(options.Password);
                });
                cfg.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
            });
        });

        return services;
    }
}
