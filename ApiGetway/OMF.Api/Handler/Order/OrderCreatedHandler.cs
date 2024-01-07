using OMF.Api.DTO.Repositories;
using OMF.Common.Events;
using OMF.Common.Events.Order;
using System.Threading.Tasks;

namespace OMF.Api.Handler.Order
{
    public class OrderCreatedHandler : IEventHandler<OrderAdded>
    {
        private readonly IOrderRepository _repository;

        public OrderCreatedHandler(IOrderRepository repository)
        {
            _repository = repository;
        }
        public async Task HandleAsync(OrderAdded @event)
        {
            await _repository.UpdateOrder(new DTO.Models.Order()
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
