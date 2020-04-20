using System;

namespace OMF.Common.Commands.Review
{
    public class DeleteReview : ICommand
    {
        public Guid OrderId { get; set; }
        public Guid ReviewId { get; set; }
    }
}
