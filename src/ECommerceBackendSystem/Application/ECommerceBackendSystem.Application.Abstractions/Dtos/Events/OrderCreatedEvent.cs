namespace ECommerceBackendSystem.Application.Abstractions.Dtos.Events;

public class OrderCreatedEvent
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public string PaymentMethod { get; set; }
    public Guid UserId { get; set; }
    public int IsActive { get; set; }
}
