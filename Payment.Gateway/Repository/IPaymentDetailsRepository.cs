using Payment.Gateway.Domain.Logs;
using System;
using System.Collections.Generic;

namespace Payment.Gateway.Repository
{
    public interface IPaymentDetailsRepository
    {
        PaymentDetails GetTransactionDetails(Guid merchantId);

        bool SaveTransactionDetails(PaymentLogDetails transactionDetails);
    }
}
