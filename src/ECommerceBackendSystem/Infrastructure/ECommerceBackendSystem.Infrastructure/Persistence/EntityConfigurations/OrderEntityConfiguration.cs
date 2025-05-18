using ECommerceBackendSystem.Domain.Entities.Orders;
using ECommerceBackendSystem.Domain.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceBackendSystem.Infrastructure.Persistence.EntityConfigurations;

public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("ORDER");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("ID").HasColumnType("uniqueidentifier").HasDefaultValueSql("NEWSEQUENTIALID()");
        builder.Property(x => x.ProductId).HasColumnName("PRODUCT_ID").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.Quantity).HasColumnName("QUANTITY").HasColumnType("integer").IsRequired();
        builder.Property(x => x.PaymentMethod).HasColumnName("PAYMENT_METHOD").HasColumnType("varchar").IsUnicode(false).HasMaxLength(50).IsRequired();
        builder.Property(x => x.UserId).HasColumnName("USER_ID").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.IsActive).HasColumnName("SW_ACTIVE").HasColumnType("integer").IsRequired();

        builder.HasQueryFilter(x => x.IsActive == (int)ActiveFlag.Active);
    }
}
