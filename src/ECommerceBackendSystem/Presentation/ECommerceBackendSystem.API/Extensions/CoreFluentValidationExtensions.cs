using FluentValidation;
using FluentValidation.AspNetCore;

namespace ECommerceBackendSystem.API.Extensions;

public static class CoreFluentValidationExtensions
{
    public static void AddCoreFluentValidation<T>(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(x =>
        {
            x.DisableDataAnnotationsValidation = true;
        });
        services.AddValidatorsFromAssemblyContaining<T>();
    }
}
