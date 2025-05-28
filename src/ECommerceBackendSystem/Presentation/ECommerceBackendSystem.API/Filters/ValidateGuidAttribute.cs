using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerceBackendSystem.API.Filters;

public class ValidateGuidAttribute : ActionFilterAttribute
{
    private readonly string _parameterName;

    public ValidateGuidAttribute(string parameterName)
    {
        _parameterName = parameterName;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue(_parameterName, out var value))
        {
            var stringValue = value?.ToString();

            if (string.IsNullOrWhiteSpace(stringValue) || 
                !Guid.TryParse(stringValue, out var parsedGuid) || 
                parsedGuid == Guid.Empty)
            {
                context.Result = new BadRequestObjectResult($"{_parameterName} must be a valid, non-empty GUID.");
            }
        }

        base.OnActionExecuting(context);
    }
}