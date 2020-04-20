using Microsoft.EntityFrameworkCore;
using OMF.CustomerManagement.Domain.Models;

namespace OMF.CustomerManagement.Domain.Repositories
{
    public class OMFIdentityDbContext : DbContext
    {
        public OMFIdentityDbContext(DbContextOptions identityDbContext) : base(identityDbContext) { }
        public DbSet<User> users { get; set; }

    }
}
