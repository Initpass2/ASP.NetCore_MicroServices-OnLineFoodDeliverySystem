using System;

namespace OMF.Common.Events.Review
{
    public class ReviewRejected : IRejectedEvent
    {
        public string Reason { get; }
        public string Code { get; }
        public string UserName { get; }
        public Guid OrderId { get; }

        protected ReviewRejected() { }

        public ReviewRejected(string reason, string code, Guid orderId)
        {
            OrderId = orderId;
            Reason = reason;
            Code = code;
        }
        public ReviewRejected(string reason, string code, string username, Guid orderId)
        {
            UserName = username;
            OrderId = orderId;
            Reason = reason;
            Code = code;
        }
    }
}
