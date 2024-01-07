using OMF.Api.DTO.Repositories;
using OMF.Common.Events;
using OMF.Common.Events.Delivery;
using OMF.Common.Events.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMF.Api.Handler.Order
{
    public class OrderUpdatedHandler : IEventHandler<OrderModified>
    {
        private readonly IOrderRepository _repository;

        public OrderUpdatedHandler(IOrderRepository repository)
        {
            _repository = repository;
        }
        public async Task HandleAsync(OrderModified @event)
        {
            await _repository.AddOrder(new DTO.Models.Order()
            {
                Address = @event.Address,
                Email = @event.Email,
                MasterOrderId = @event.OrderId,
                OrderItem = @event.OrderItem,
                OrderStatus = @event.OrderStatus,
                PhoneNumber = @event.PhoneNumber,
                RestaurantId = @event.RestaurantId,
                TotalPrice = @event.TotalPrice,
                UserId = @event.UserId,
                UserName = @event.UserName
            });
        }
    }
}
