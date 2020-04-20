using OMF.OrderManagement.Domain;
using System;
using System.Threading.Tasks;

namespace OMF.OrderManagement.Service
{
    public interface IOrderService
    {
        Task CreateOrderAsync(Orderdetils order,int paymentId);
        Task UpdateOrderAsync(Orderdetils order, int paymentId);
        Task DeleteOrderAsync(Guid orderId);
        Task<Order> GetOrderAsync(Guid orderId);
    }
}
