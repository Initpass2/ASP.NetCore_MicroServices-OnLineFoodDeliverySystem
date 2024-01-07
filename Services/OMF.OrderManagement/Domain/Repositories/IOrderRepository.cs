using System;
using System.Threading.Tasks;

namespace OMF.OrderManagement.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task Update(Order order);
        Task Delete(Order orderId);
        Task<Order> GetAsync(Guid orderId);
    }
}
