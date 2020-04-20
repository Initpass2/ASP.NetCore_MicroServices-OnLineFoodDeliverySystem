using Microsoft.EntityFrameworkCore;
using OMF.CustomerManagement.Domain.Models;
using System;
using System.Threading.Tasks;

namespace OMF.CustomerManagement.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly OMFIdentityDbContext _context;

        public UserRepository(OMFIdentityDbContext actioIdentiyDbContext)
        {
            _context = actioIdentiyDbContext;
        }
        public async Task AddAsync(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
             _context.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await _context.users.SingleOrDefaultAsync(i => i.Id == id);
        }

        public async Task<User> GetAsync(string email)
        {
            return await _context.users.SingleOrDefaultAsync(e => e.Email == email);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
