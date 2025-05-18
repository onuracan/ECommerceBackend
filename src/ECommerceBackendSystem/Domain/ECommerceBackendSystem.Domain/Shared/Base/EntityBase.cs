namespace ECommerceBackendSystem.Domain.Shared.Base;

public class EntityBase
{
    public virtual Guid Id { get; set; }
    public virtual int IsActive { get; set; }
}
