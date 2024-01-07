using System;

namespace OMF.Common.Commands.Review
{
    public class UpdateReview : ICommand
    {
        public Guid ReviewId { get; set; }
        public Guid OrderId { get; set; }
        public decimal Rating { get; set; }
        public string TextReview { get; set; }
    }
}
