using Microsoft.EntityFrameworkCore;
using OMF.OrderManagement.Domain.EntityTypeConfiguration;

namespace OMF.OrderManagement.Domain.Repositories
{
    public class OMFOrderDbContext : DbContext
    {
        public OMFOrderDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        public DbSet<Order> order { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<PaymentInfo> paymentDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentDetailsEntityTypeConfiguration());
        }
    }
}
