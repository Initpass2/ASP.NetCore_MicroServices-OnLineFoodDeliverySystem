using System;

namespace OMF.OrderManagement.Service
{
    public class OrderManagementDataModel
    {
        public Customerdetails CustomerDetails { get; set; }
        public Paymentdetails PaymentDetails { get; set; }
        public Orderdetils OrderDetils { get; set; }
    }

    public class Customerdetails
    {
        public int CustomerId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Shippingaddress ShippingAddress { get; set; }
    }

    public class Shippingaddress
    {
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Address { get; set; }
    }

    public class Paymentdetails
    {
        public int PaymentId { get; set; }
        public string CardHolderName { get; set; }
        public string Alis { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public CardType CardType { get; set; }
    }

    public class Orderdetils
    {
        public Guid? OrderId { get; set; }
        public int RestaurantId { get; set; }
        public string OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Orderitem[] OrderItem { get; set; }
        public float TotalPrice { get; set; }
    }

    public class Orderitem
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int Discount { get; set; }
        public float UnitPrice { get; set; }
        public int Unit { get; set; }
    }
    public enum CardType
    {
        AMEX,
        VISA,
        MASTER
    }
    public enum OrderStatus
    {
        Submitted,
        AwaitingValidation,
        StockConfirmed,
        Paid,
        Shipped,
        Cancelled
    }
}
