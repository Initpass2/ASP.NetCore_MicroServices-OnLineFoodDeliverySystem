using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OMF.OrderManagement.Domain.EntityTypeConfiguration
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Customer");
            entityTypeBuilder.HasKey(cr => cr.CustomerId);
            entityTypeBuilder.Property(cr => cr.Name).IsRequired();
            entityTypeBuilder.Property(cr => cr.Email).IsRequired();
            entityTypeBuilder.Property(cr => cr.PhoneNumber).IsRequired();
            entityTypeBuilder.Property(cr=>cr.Shippingaddress).IsRequired();
        }
    }
}
