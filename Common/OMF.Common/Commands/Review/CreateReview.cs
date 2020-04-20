using System;

namespace OMF.Common.Commands.Review
{
    public class CreateReview : ICommand
    {
        public string TextReview { get; set; }
        public string UserName { get; set; }
        public decimal Rating { get; set; }
        public Guid OrderId { get; set; }
        public string ResturentName { get; set; }
        public int ResturentID { get; set; }
    }
}
