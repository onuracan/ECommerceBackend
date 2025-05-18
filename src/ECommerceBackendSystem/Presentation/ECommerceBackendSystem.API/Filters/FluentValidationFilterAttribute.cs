using ECommerceBackendSystem.Application.Abstractions.Services.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace ECommerceBackendSystem.API.Filters;

public class FluentValidationFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
            return;

        var errorBuilder = new StringBuilder();

        foreach (var error in context.ModelState.Values.SelectMany(x => x.Errors))
            errorBuilder.AppendLine($"{error.ErrorMessage}");

        var serviceResponseHelper = this.GetServiceResponseHelper(context);

        context.Result = new BadRequestObjectResult(null)
        {
            StatusCode = StatusCodes.Status400BadRequest,
            Value = serviceResponseHelper.Fail(errorBuilder.ToString())
        };

        base.OnActionExecuting(context);
    }

    private IServiceResponseHelper GetServiceResponseHelper(ActionExecutingContext context)
        => context.HttpContext.RequestServices.GetRequiredService<IServiceResponseHelper>();
}
