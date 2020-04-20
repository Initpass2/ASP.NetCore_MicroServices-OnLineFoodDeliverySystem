using System.Linq;
using System.Threading.Tasks;

namespace OMF.OrderManagement.Domain.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly OMFOrderDbContext _dbContext;

        public PaymentRepository(OMFOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(PaymentInfo payment)
        {
            _dbContext.Add(payment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(PaymentInfo payment)
        {
            _dbContext.Update(payment);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<PaymentInfo> FindByCardNumberAsync(string cardNumber)
        {
            return _dbContext.paymentDetails.Where(c => c.CardNumber.Equals(cardNumber)).SingleOrDefault();
        }

        public async Task<PaymentInfo> FindByIdAsync(int paymentId)
        {
            return await _dbContext.paymentDetails.FindAsync(paymentId);
        }
    }
}
