using Payment.Gateway.Domain.CardManagement;
using Payment.Gateway.Domain.Logs;
using Payment.Gateway.Domain.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Gateway.Service
{
    public interface IPaymentService
    {
        PaymentResponse ProcessTransaction(PaymentTransactionDetails paymentTransactionDetails, string merchantCardNum);

        PaymentDetails GetPaymentHistory(Guid merchantId);

        bool UpdatePaymentHistory(PaymentResponse paymentResponse);

        Card CheckCard(PaymentTransactionDetails paymentTransactionDetails);

        
    }
}
