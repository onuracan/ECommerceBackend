using System.Net;

namespace ECommerceBackendSystem.Application.Abstractions.Dtos.ServiceResponse;

public class ServiceResponse<T> : BaseResponse
{
    public T Data { get; set; }
}
