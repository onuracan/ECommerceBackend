using ECommerceBackendSystem.Application.Abstractions.Dtos.ServiceResponse;
using ECommerceBackendSystem.Application.Abstractions.Services.Base;
using System.Net;

namespace ECommerceBackendSystem.Application.Base;

public class ServiceResponseHelper : IServiceResponseHelper
{
    public ServiceResponse<T> Success<T>(T data, string message = null)
    {
        return new ServiceResponse<T>
        {
            IsSuccessful = true,
            StatusCode = HttpStatusCode.OK,
            Data = data,
            Message = message
        };
    }

    public ServiceResponse Success(string message = null)
    {
        return new ServiceResponse
        {
            IsSuccessful = true,
            StatusCode = HttpStatusCode.OK,
            Message = message
        };
    }

    public ServiceResponse<T> Fail<T>(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResponse<T>
        {
            IsSuccessful = false,
            StatusCode = statusCode,
            Message = message
        };
    }

    public ServiceResponse Fail(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResponse
        {
            IsSuccessful = false,
            StatusCode = statusCode,
            Message = message
        };
    }
}
