using OMF.Api.DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMF.Api.DTO.Repositories
{
    public interface IOrderRepository
    {
        Task AddOrder(Order order);
        Task UpdateOrder(Order order);
        Task DeleteOrder(Guid orderId);
        Task<Order> GetOrder(Guid orderId);
        Task<IEnumerable<Order>> GetOrder(string userId);
    }
}
