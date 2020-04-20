using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OMF.Api.DTO.Models;

namespace OMF.Api.DTO.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OMFStorageContext _context;

        public OrderRepository(OMFStorageContext context)
        {
            _context = context;
        }

        public async Task AddOrder(Order order)
        {
            _context.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrder(Guid orderId)
        {
            var myorder = await _context.orders.Where(o => o.MasterOrderId == orderId).Select(o => o).SingleOrDefaultAsync();
            _context.Remove(myorder);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetOrder(Guid orderId)
        {
            return await _context.orders.Where(o => o.MasterOrderId == orderId).Select(o => o).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetOrder(string userId)
        {
            return await _context.orders.Where(o => o.UserId == userId).ToListAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            var myorder = await _context.orders.Where(o => o.MasterOrderId == order.MasterOrderId).Select(o => o).SingleOrDefaultAsync();
            myorder.OrderItem = order.OrderItem;
            myorder.OrderStatus = order.OrderStatus;
            myorder.PhoneNumber = order.PhoneNumber;
            myorder.TotalPrice = order.TotalPrice;
            _context.Update(myorder);
            await _context.SaveChangesAsync();
        }
    }
}
