using ECommerceBackendSystem.API.Filters;

namespace ECommerceBackendSystem.API.Extensions;

public static class FluentValidateFilterExtensions
{
    public static IMvcBuilder AddFluentValidateFilter(this IMvcBuilder builder)
    {
        builder.AddMvcOptions(x => x.Filters.Add<FluentValidationFilterAttribute>());
        return builder;
    }
}
