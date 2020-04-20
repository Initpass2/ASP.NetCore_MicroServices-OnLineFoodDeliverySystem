using OMF.Common.Events;
using OMF.Common.Events.Delivery;
using System.Threading.Tasks;

namespace OMF.OrderDeliveryManagement.Handler
{
    public class OrderUpdatedHandler : IEventHandler<NofifiedOrderUpdated>
    {
        public async Task HandleAsync(NofifiedOrderUpdated @event)
        {
            // do whatever you want herer 
            await Task.CompletedTask;
        }
    }
}
