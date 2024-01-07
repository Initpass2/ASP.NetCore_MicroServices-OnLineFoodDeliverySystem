using System;
using System.Threading.Tasks;

namespace OMF.OrderManagement.Domain.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OMFOrderDbContext _dbContext;

        public OrderRepository(OMFOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Order order)
        {
            _dbContext.Add(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Order order)
        {
            _dbContext.Update(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Order> GetAsync(Guid orderId)
        {
          return  await _dbContext.order.FindAsync(orderId);
        }

        public async Task Delete(Order order)
        {
            _dbContext.Remove(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}
