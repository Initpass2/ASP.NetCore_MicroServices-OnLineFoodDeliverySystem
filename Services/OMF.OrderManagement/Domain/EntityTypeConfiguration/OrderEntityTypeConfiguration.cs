using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OMF.OrderManagement.Domain.EntityTypeConfiguration
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Order");
            entityTypeBuilder.HasKey(cr => cr.OrderId);
            entityTypeBuilder.HasOne<PaymentInfo>().WithMany().HasForeignKey("PaymentId");          
            entityTypeBuilder.Property(cr => cr.OrderDate).IsRequired();
            entityTypeBuilder.Property(cr => cr.Orderitem).IsRequired();
            entityTypeBuilder.Property(cr => cr.Orderstatus).IsRequired();           
            entityTypeBuilder.Property(cr => cr.TotalPrice).IsRequired();
            entityTypeBuilder.Property(cr => cr.RestaurentId).IsRequired();
        }
    }
}
