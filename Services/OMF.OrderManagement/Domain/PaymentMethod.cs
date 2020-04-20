using System;

namespace OMF.OrderManagement.Domain
{
    public class PaymentInfo
    {       
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CardHolderName { get; set; }
        public string Alis { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string Cardtype { get; set; }
    }
  
}
