using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OMF.OrderManagement.Domain.EntityTypeConfiguration
{
    public class PaymentDetailsEntityTypeConfiguration : IEntityTypeConfiguration<PaymentInfo>
    {
        public void Configure(EntityTypeBuilder<PaymentInfo> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("PaymentDeatils");
            entityTypeBuilder.HasKey(cr => cr.Id);
            entityTypeBuilder.Property(cr => cr.Expiration).IsRequired();
            entityTypeBuilder.HasOne<Customer>().WithOne().HasForeignKey<PaymentInfo>(x => x.CustomerId);
            entityTypeBuilder.Property(cr => cr.Cardtype).IsRequired();    
            entityTypeBuilder.Property(cr => cr.CardNumber).IsRequired();
            entityTypeBuilder.Property(cr => cr.CardHolderName).IsRequired();
            entityTypeBuilder.Property(cr => cr.Alis).IsRequired();           
        }
    }
}