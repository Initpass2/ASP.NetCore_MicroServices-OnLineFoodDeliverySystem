using Newtonsoft.Json;
using OMF.Common.Exception;
using OMF.OrderManagement.Domain;
using OMF.OrderManagement.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace OMF.OrderManagement.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateOrderAsync(Orderdetils order, int paymentId)
        {
            try
            {
             
                await _repository.AddAsync(new Order()
                {
                    OrderId = order.OrderId.Value,
                    OrderDate = order.OrderDate,
                    Orderitem = JsonConvert.SerializeObject(order.OrderItem),
                    Orderstatus = order.OrderStatus.ToString(),
                    RestaurentId = order.RestaurantId,
                    PaymentId = paymentId,
                    TotalPrice = order.TotalPrice
                });
            }
            catch (Exception ex)
            {
                throw new OMFException("", ex.Message);
            }

        }

        public async Task DeleteOrderAsync(Guid orderId)
        {
            var orderDetails = await _repository.GetAsync(orderId);
            if (orderDetails == null)
                throw new OMFException("No record found", $"No order recors found by orderId {orderId}");
            await _repository.Delete(orderDetails);
        }

        public async Task<Order> GetOrderAsync(Guid orderId)
        {
          var order =  await _repository.GetAsync(orderId);
            if(order == null)
                throw new OMFException("No record found", $"No order recors found by orderId {orderId}");
            return order;
        }

        public async Task UpdateOrderAsync(Orderdetils order, int paymentId)
        {
            try
            {
                var orderdetails = await _repository.GetAsync(order.OrderId.Value);
                if (orderdetails == null)
                    throw new OMFException("", "");

                orderdetails.OrderDate = orderdetails.OrderDate;
                orderdetails.Orderitem = JsonConvert.SerializeObject(order.OrderItem);
                orderdetails.Orderstatus = order.OrderStatus.ToString();
                orderdetails.PaymentId = paymentId;
                orderdetails.RestaurentId = order.RestaurantId;
                orderdetails.TotalPrice = order.TotalPrice;

                await _repository.Update(orderdetails);

            }
            catch (Exception ex)
            {
                throw new OMFException("", ex.Message);
            }

        }
    }
}
