using OMF.CustomerManagement.Domain.Models;
using System;
using System.Threading.Tasks;

namespace OMF.CustomerManagement.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
