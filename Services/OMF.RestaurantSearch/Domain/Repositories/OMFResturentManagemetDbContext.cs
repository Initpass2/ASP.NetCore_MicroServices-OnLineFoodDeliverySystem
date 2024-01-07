using Microsoft.EntityFrameworkCore;
using OMF.RestaurantManagement.Domain.Models;
using OMF.RestaurantSearch.Domain.Models;

namespace OMF.RestaurantManagement.Domain.Repositories
{
    public class OMFRestaurantManagemetDbContext : DbContext
    {
        public OMFRestaurantManagemetDbContext(DbContextOptions identityDbContext) : base(identityDbContext) { }
        public DbSet<Budget> budgets { get; set; }
        public DbSet<Cuisine> cuisines { get; set; }
        public DbSet<Menu> menus { get; set; }
        public DbSet<Restaurant> restaurants { get; set; }
        public DbSet<RestaurantMenuItem> restaurantMenuItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RestaurantMenuItem>()
            .HasIndex(r => new { r.MenuID, r.ResturantID }).IsUnique();
        }
    }
}