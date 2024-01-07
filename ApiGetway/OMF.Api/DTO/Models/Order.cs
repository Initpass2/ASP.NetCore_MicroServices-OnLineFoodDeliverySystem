using System;
using System.ComponentModel.DataAnnotations;

namespace OMF.Api.DTO.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public Guid MasterOrderId { get; set; }
        public string OrderItem { get; set; }
        public float TotalPrice { get; set; }
        public int RestaurantId { get; set; }
        public string OrderStatus { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}
