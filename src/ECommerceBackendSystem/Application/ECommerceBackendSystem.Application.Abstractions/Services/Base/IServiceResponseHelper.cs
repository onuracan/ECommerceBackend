using ECommerceBackendSystem.Application.Abstractions.Dtos.ServiceResponse;
using System.Net;

namespace ECommerceBackendSystem.Application.Abstractions.Services.Base;

public interface IServiceResponseHelper
{
    ServiceResponse<T> Success<T>(T data, string message = null);
    ServiceResponse Success(string message = null);

    ServiceResponse<T> Fail<T>(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest);
    ServiceResponse Fail(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest);
}
