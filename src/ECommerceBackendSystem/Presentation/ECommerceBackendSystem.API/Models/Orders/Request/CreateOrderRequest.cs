namespace ECommerceBackendSystem.API.Models.Orders.Request;

public class CreateOrderRequest
{
    public string UserId { get; set; }
    public string ProductId { get; set; }
    public int Quantity { get; set; }
    public string PaymentMethod { get; set; }
}
