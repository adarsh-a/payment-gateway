using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Payment.Gateway.Domain.Logs;

namespace Payment.Gateway.Repository
{
    public class PaymentDetailsRepository : IPaymentDetailsRepository
    {
        private List<LogDetails> logEntries;

        public PaymentDetailsRepository()
        {
            logEntries = new List<LogDetails>()
            {
                new LogDetails()
                {
                    Amount=19.0,
                    CreatedDate=DateTime.Now.AddDays(-10),
                    Identifier = Guid.NewGuid(),
                    MerchantId = new Guid("a731bea3-d82f-4cdb-8405-c91e4a4603f4"),
                    PayerCardNum = "4550123412342345",
                    Status=false
                } ,
                new LogDetails()
                {
                    Amount=19.0,
                    CreatedDate=DateTime.Now.AddDays(-10),
                    Identifier = Guid.NewGuid(),
                    MerchantId = new Guid("a731bea3-d82f-4cdb-8405-c91e4a4603f4"),
                    PayerCardNum = "4550123412342345",
                    Status=true
                },
                 new LogDetails()
                {
                    Amount=15.0,
                    CreatedDate=DateTime.Now.AddDays(-5),
                    Identifier = Guid.NewGuid(),
                    MerchantId = new Guid("a731bea3-d82f-4cdb-8405-c91e4a4603f4"),
                    PayerCardNum = "4550123812342345",
                    Status=true
                },
                 new LogDetails()
                {
                    Amount=150.0,
                    CreatedDate=DateTime.Now.AddDays(-8),
                    Identifier = Guid.NewGuid(),
                    MerchantId = new Guid("d5ac41ea-32ef-464a-adef-847ecfcd02fd"),
                    PayerCardNum = "4550123812342345",
                    Status=true
                }
            };

        }

        public PaymentDetails GetTransactionDetails(Guid merchantId)
        {
            var paymentDetails = new PaymentDetails();
            var list = logEntries.Where(i => i.MerchantId == merchantId).ToList();

            if (list.Any())
            {
                paymentDetails.Payments = list;
            }
            else
            {
                paymentDetails.Message = $"No transactions found for merchant with id {merchantId}";
            }

            return paymentDetails;
        }

        public bool SaveTransactionDetails(LogDetails transactionDetails)
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
