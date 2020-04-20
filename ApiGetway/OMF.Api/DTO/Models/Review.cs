using System;
using System.ComponentModel.DataAnnotations;

namespace OMF.Api.DTO.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public Guid MasterReviewId { get; set; }
        public string ReviewText { get; set; }
        public decimal Rating { get; set; }
        public Guid OrderId { get; set; }
        public int RestaurantId { get; set; }
        public string UserName { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
