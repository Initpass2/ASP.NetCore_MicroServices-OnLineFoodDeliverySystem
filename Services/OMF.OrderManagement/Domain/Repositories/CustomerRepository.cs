using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMF.OrderManagement.Domain.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly OMFOrderDbContext _dbContext;

        public CustomerRepository(OMFOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Customer customer)
        {
            _dbContext.Add(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Customer customer)
        {
            _dbContext.Update(customer);
            await _dbContext.SaveChangesAsync();
        }        

        public async Task<IEnumerable<Customer>> FindByUserIdAsync(string userId)
        {
            return await _dbContext.customers.Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<Customer> FindByCustomerIdAsync(int customerId)
        {
            return await _dbContext.customers.FindAsync(customerId);
        }
    }
}
