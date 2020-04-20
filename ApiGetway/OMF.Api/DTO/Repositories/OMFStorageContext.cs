using Microsoft.EntityFrameworkCore;
using OMF.Api.DTO.Models;

namespace OMF.Api.DTO.Repositories
{
    public class OMFStorageContext : DbContext
    {
        public OMFStorageContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        public DbSet<Restaurant> restaurants { get; set; }
        public DbSet<Review> reviews { get; set; }
        public DbSet<Order> orders { get; set; }
    }
}
