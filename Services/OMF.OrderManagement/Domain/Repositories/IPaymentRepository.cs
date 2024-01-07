using System.Threading.Tasks;

namespace OMF.OrderManagement.Domain.Repositories
{
    public interface IPaymentRepository
    {
        Task AddAsync(PaymentInfo payment);
        Task UpdateAsync(PaymentInfo payment);        
        Task<PaymentInfo> FindByIdAsync(int paymentId);
        Task<PaymentInfo> FindByCardNumberAsync(string cardNumber);
    }
}
