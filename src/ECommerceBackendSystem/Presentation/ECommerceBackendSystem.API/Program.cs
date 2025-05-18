using ECommerceBackendSystem.API.Extensions;
using ECommerceBackendSystem.Application.Mappings;
using ECommerceBackendSystem.Infrastructure.Caching.Extensions;
using ECommerceBackendSystem.Infrastructure.Messaging.Extensions;
using ECommerceBackendSystem.Infrastructure.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions().AddFluentValidateFilter();
builder.Services.AddCoreFluentValidation<Program>();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddRabbitMq(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddAutoMapper(typeof(Program), typeof(OrderProfile));
builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCustomCors();
builder.Services.AddHttpContextAccessor();
builder.Host.UseCustomSeriLog();

if (builder.Environment.IsDevelopment())
    builder.Services.AddCustomSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseCustomSwagger();
else
    app.UseHttpsRedirection();

app.UseCustomMiddlewares();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
