using System;

namespace OMF.Common.Events.Review
{
    public class ReviewUpdated : IEvent
    {
        public string TextReview { get; set; }
        public decimal Rating { get; set; }
        public string ResturentName { get; set; }
        public int ResturentId { get; set; }
        public Guid OrderId { get; set; }
        public Guid ReviewId { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UserName { get; set; }

        public ReviewUpdated(string textReview, decimal rating, string resturentname, int resturentid, string userName, DateTime updatedOn, Guid orderId, Guid reviewId)
        {
            TextReview = textReview;
            Rating = rating;
            ResturentName = resturentname;
            ResturentId = resturentid;
            OrderId = orderId;
            ReviewId = reviewId;
            UpdatedOn = updatedOn;
            UserName = userName;
        }

        protected ReviewUpdated()
        {

        }
    }
}
