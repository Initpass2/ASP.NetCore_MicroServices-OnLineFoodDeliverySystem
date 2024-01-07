using System;

namespace OMF.OrderManagement.Domain
{
    public class Order
    {       
        public Guid OrderId { get; set; }     
        public int PaymentId { get; set; }
        public int RestaurentId { get; set; }
        public string OrderDate { get; set; }
        public string Orderstatus { get; set; }
        public string Orderitem { get; set; }
        public float TotalPrice { get; set; }       
    }     
}
