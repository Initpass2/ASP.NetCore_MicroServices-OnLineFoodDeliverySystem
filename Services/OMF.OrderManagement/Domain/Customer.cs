namespace OMF.OrderManagement.Domain
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Shippingaddress { get; set; }
    }
}
//    public class ShippingAddress
//    {
//        public string City { get; set; }
//        public string State { get; set; }
//        public string Zip { get; set; }
//        public string Address { get; set; }
//    }
//}
