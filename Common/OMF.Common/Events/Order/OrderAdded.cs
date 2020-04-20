using System;

namespace OMF.Common.Events.Order
{
    public class OrderAdded : IEvent
    {
        public Guid OrderId { get; set; }
        public string OrderItem { get; set; }
        public float TotalPrice { get; set; }
        public int RestaurantId { get; set; }
        public string OrderStatus { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }

        public OrderAdded(Guid orderId, string orderItem, float totalPrice, int restaurantId, string orderstatus, string address,
            string username, string phoneNumber, string userId, string email)
        {
            OrderId = orderId;
            OrderItem = orderItem;
            TotalPrice = totalPrice;
            RestaurantId = restaurantId;
            OrderStatus = orderstatus;
            Address = address;
            UserName = username;
            PhoneNumber = phoneNumber;
            UserId = userId;
            Email = email;
        }

        protected OrderAdded()
        {
        }
    }
}
