using OMF.Api.DTO.Repositories;
using OMF.Common.Events;
using OMF.Common.Events.Order;
using System.Threading.Tasks;

namespace OMF.Api.Handler.Order
{
    public class OrderDeletedHandler : IEventHandler<OrderCanceled>
    {
        private readonly IOrderRepository _repository;

        public OrderDeletedHandler(IOrderRepository repository)
        {
            _repository = repository;
        }
        
        public async Task HandleAsync(OrderCanceled @event)
        {
           await _repository.DeleteOrder(@event.OrderId);
        }
    }
}