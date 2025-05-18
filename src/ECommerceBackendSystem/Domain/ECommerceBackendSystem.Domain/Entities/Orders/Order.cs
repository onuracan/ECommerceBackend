using ECommerceBackendSystem.Domain.Shared.Base;

namespace ECommerceBackendSystem.Domain.Entities.Orders;

public class Order : EntityBase
{
    public virtual Guid ProductId { get; set; }
    public virtual int Quantity { get; set; }
    public virtual string PaymentMethod { get; set; }
    public virtual Guid UserId { get; set; }
}
