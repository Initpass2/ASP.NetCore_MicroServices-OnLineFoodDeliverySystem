using OMF.OrderManagement.Domain;
using System.Threading.Tasks;

namespace OMF.OrderManagement.Service
{
    public interface IPaymentService
    {
        Task CreatePaymentAsync(Paymentdetails payment, int customerId);
        Task UpdatePaymentAsync(Paymentdetails payment,int customerId);
        Task<PaymentInfo> FindByIdAsync(int paymentId);
        Task<PaymentInfo> FindByCardNumberAsync(string cardnumber);
    }
}
