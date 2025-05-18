namespace ECommerceBackendSystem.Application.Abstractions.Dtos.Orders;

public class CreateOrderDto
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public string PaymentMethod { get; set; }
}
