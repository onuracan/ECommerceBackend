using System.Net;

namespace ECommerceBackendSystem.Application.Abstractions.Dtos.ServiceResponse;

public abstract class BaseResponse
{
    public bool IsSuccessful { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
}
