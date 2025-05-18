using ECommerceBackendSystem.Infrastructure.Caching.Extensions;
using ECommerceBackendSystem.OrderProcessingWorker;
using ECommerceBackendSystem.OrderProcessingWorker.Options;
using MassTransit;
using Microsoft.Extensions.Options;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger());

builder.Services.AddRedisCache(builder.Configuration);

builder.Services.Configure<RabbitMqOptions>(x =>
{
    x.HostName = builder.Configuration["RabbitMq:HostName"];
    x.UserName = builder.Configuration["RabbitMq:UserName"];
    x.Password = builder.Configuration["RabbitMq:Password"];
    x.QueueName = builder.Configuration["RabbitMq:QueueName"];
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderPlacedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        var options = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;
        cfg.Host(options.HostName, "/", h =>
        {
            h.Username(options.UserName);
            h.Password(options.Password);
        });

        cfg.ReceiveEndpoint(options.QueueName, e =>
        {
            e.Durable = true;
            e.AutoDelete = false;
            e.ConfigureConsumer<OrderPlacedConsumer>(context);
            e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
        });
    });
});

var host = builder.Build();

host.Run();
