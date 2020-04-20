using System;
using System.ComponentModel.DataAnnotations;

namespace OMF.ReviewManagement.Domain
{
    public class Review
    {
        [Required]       
        public Guid ReviewId { get; set; }
        [Required]
        public string ReviewText { get; set; }
        [Required]
        public string ModeratorTrackingId { get; set; }     
        public string UserName { get; set; }
        [Required]
        public Guid OrderId { get; set; }            
        [Required]
        public string ResturentName { get; set; }
        public int ResturentID { get; set; }
        public decimal Rating { get; set; }       
        public bool IsManualReviewRequired { get; set; }       
        [Required]
        public DateTime UpdatedOn { get; set; }

    }
}
