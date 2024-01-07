using System;

namespace OMF.Common.Events.Review
{
    public class ReviewDeleted : IEvent
    {
        public Guid ReviewId { get; set; }
        public Guid OrderId { get; set; }
        protected ReviewDeleted() { }
        public ReviewDeleted(Guid reviewId, Guid orderId) { ReviewId = reviewId; OrderId = orderId; }
    }
}
