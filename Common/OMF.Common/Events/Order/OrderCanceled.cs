using System;

namespace OMF.Common.Events.Order
{
    public class OrderCanceled : IEvent
    {
        public Guid OrderId { get; set; }
        public OrderCanceled(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
