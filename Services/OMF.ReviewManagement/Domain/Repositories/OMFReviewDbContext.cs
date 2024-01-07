using Microsoft.EntityFrameworkCore;

namespace OMF.ReviewManagement.Domain.Repositories
{
    public class OMFReviewDbContext : DbContext
    {
        public OMFReviewDbContext(DbContextOptions reviewDbContext) : base(reviewDbContext) { }
        public DbSet<Review> reviews { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
            .HasIndex(r => new { r.ReviewId, r.OrderId }).IsUnique();
        }
    }
}
