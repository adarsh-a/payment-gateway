using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Payment.Gateway.Domain.Logs;

namespace Payment.Gateway.Repository
{
    public class PaymentDetailsRepository : IPaymentDetailsRepository
    {
        private List<PaymentLogDetails> logEntries;

        public PaymentDetailsRepository()
        {
            logEntries = new List<PaymentLogDetails>()
            {
                new PaymentLogDetails()
                {
                    Amount=19.0,
                    CreatedDate=DateTime.Now.AddDays(-10),
                    Identifier = Guid.NewGuid(),
                    MerchantId = new Guid("a731bea3-d82f-4cdb-8405-c91e4a4603f4"),
                    PayerCardNum = "4550123412342345",
                    Status=false
                } ,
                new PaymentLogDetails()
                {
                    Amount=19.0,
                    CreatedDate=DateTime.Now.AddDays(-10),
                    Identifier = Guid.NewGuid(),
                    MerchantId = new Guid("a731bea3-d82f-4cdb-8405-c91e4a4603f4"),
                    PayerCardNum = "4550123542342345",
                    Status=true
                },
                 new PaymentLogDetails()
                {
                    Amount=15.0,
                    CreatedDate=DateTime.Now.AddDays(-5),
                    Identifier = Guid.NewGuid(),
                    MerchantId = new Guid("a731bea3-d82f-4cdb-8405-c91e4a4603f4"),
                    PayerCardNum = "4550123812002345",
                    Status=true
                },
                 new PaymentLogDetails()
                {
                    Amount=150.0,
                    CreatedDate=DateTime.Now.AddDays(-8),
                    Identifier = Guid.NewGuid(),
                    MerchantId = new Guid("d5ac41ea-32ef-464a-adef-847ecfcd02fd"),
                    PayerCardNum = "4550123772342345",
                    Status=true
                }
            };

        }

        public PaymentDetails GetTransactionDetails(Guid merchantId)
        {
            var paymentDetails = new PaymentDetails();
            var list = logEntries.Where(i => i.MerchantId == merchantId).ToList();
            paymentDetails.Payments = new List<PaymentLogDetails>();

            if (list.Any())
            {
                foreach(var paymentDetail in list) 
                {
                    if (paymentDetail.PayerCardNum.Length > 11)
                    {
                        var maskedCard = $"{paymentDetail.PayerCardNum.Substring(0, 6)}xxxxxx{paymentDetail.PayerCardNum.Substring(12)}";
                        paymentDetail.PayerCardNum = maskedCard;
                    }

                    paymentDetails.Payments.Add(paymentDetail);


                }
            }
            else
            {
                paymentDetails.Message = $"No transactions found for merchant with id {merchantId}";
            }

            return paymentDetails;
        }

        public bool SaveTransactionDetails(PaymentLogDetails transactionDetails)
        {
            try
            {
                logEntries.Add(transactionDetails);
                var entry = logEntries.FirstOrDefault(i => i.Identifier == transactionDetails.Identifier);
                if (entry == null) return false;

                return true;
            }
            catch (Exception e)
            {
                //log
                return false;

            }
        }
    }
}
