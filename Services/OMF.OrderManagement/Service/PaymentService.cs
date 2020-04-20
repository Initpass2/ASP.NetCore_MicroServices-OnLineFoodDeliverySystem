using OMF.Common.Exception;
using OMF.OrderManagement.Domain;
using OMF.OrderManagement.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace OMF.OrderManagement.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;
        public PaymentService(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task CreatePaymentAsync(Paymentdetails payment, int customerId)
        {
            var paymentDeatils = await _repository.FindByCardNumberAsync(payment.CardNumber);
            if (paymentDeatils == null)
            {
                await _repository.AddAsync(new PaymentInfo()
                {
                    CustomerId = customerId,
                    Alis = payment.Alis,
                    CardHolderName = payment.CardHolderName,
                    CardNumber = payment.CardNumber,
                    Cardtype = payment.CardType.ToString(),
                    Expiration = payment.Expiration,

                });
            }
        }

        public async Task<PaymentInfo> FindByCardNumberAsync(string cardnumber)
        {
            return await _repository.FindByCardNumberAsync(cardnumber);
        }

        public async Task<PaymentInfo> FindByIdAsync(int paymentId)
        {
            return await _repository.FindByIdAsync(paymentId);
        }

        public async Task UpdatePaymentAsync(Paymentdetails payment, int customerId)
        {
            var paymentDbEntity = await _repository.FindByIdAsync(payment.PaymentId);
            if (paymentDbEntity == null)
                throw new OMFException("No paymentdetails found", $"No payment found for billing");

            paymentDbEntity.Alis = payment.Alis;
            paymentDbEntity.CardHolderName = payment.CardHolderName;
            paymentDbEntity.CardNumber = payment.CardNumber;
            paymentDbEntity.Cardtype = payment.CardType.ToString();
            paymentDbEntity.CustomerId = customerId;
            paymentDbEntity.Expiration = payment.Expiration;

           await _repository.UpdateAsync(paymentDbEntity);

        }
    }
}
