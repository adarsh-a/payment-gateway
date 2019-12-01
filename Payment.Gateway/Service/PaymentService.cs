using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Payment.Gateway.Domain.Logs;
using Payment.Gateway.Domain.Payment;
using Payment.Gateway.Repository;
using RestSharp;

namespace Payment.Gateway.Service
{
    public class PaymentService : IPaymentService
    {
        private IPaymentDetailsRepository paymentRepository { get; set; }
        public PaymentService(IPaymentDetailsRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }
        public PaymentDetails GetPaymentHistory(Guid merchantId)
        {
            try
            {
                return paymentRepository.GetTransactionDetails(merchantId);
            }
            catch (Exception e)
            {
                //log error
                return new PaymentDetails { Message = "An error occured. Please try again later" };
            }
        }

        public PaymentResponse ProcessTransaction(PaymentTransactionDetails paymentTransactionDetails, string merchantCardNum)
        {
            if (!string.IsNullOrEmpty(merchantCardNum))
            {
                try
                {
                    //RestClient restClient = new RestClient("");
                    //RestRequest request = new RestRequest("/bank/transaction", Method.POST);
                    //request.AddHeader("Content-Type", "application/json");
                    //request.AddParameter("merchantcard", merchantCardNum);
                    //request.AddParameter("payeecard", paymentTransactionDetails.PayerCardNum);
                    //request.AddParameter("ccv", paymentTransactionDetails.CCV);
                    //request.AddParameter("amount", paymentTransactionDetails.Amount);
                    //request.AddParameter("expiry", paymentTransactionDetails.ExpiryDate);
                    //var test = restClient.Execute<PaymentResponse>(request);

                    var paymentResponse = new PaymentResponse
                    {
                        Amount = paymentTransactionDetails.Amount,
                        Identifier = Guid.NewGuid(),
                        MerchantId = paymentTransactionDetails.MerchantId,
                        PayerCardNum = paymentTransactionDetails.PayerCardNum,
                        Status = false

                    };

                    //fail or pass transaction randomly
                    Random random = new Random();
                    int chosenNum = random.Next(0, 10);
                    if (chosenNum % 2 == 0)
                    {
                        paymentResponse.Status = true;
                    }

                    return paymentResponse;
                }
                catch (Exception e)
                {
                    //add log

                }

            }

            return new PaymentResponse();
        }

        public bool UpdatePaymentHistory(PaymentResponse paymentResponse)
        {
            LogDetails currentTransaction = new LogDetails
            {
                Amount = paymentResponse.Amount,
                CreatedDate = DateTime.Now,
                Identifier = paymentResponse.Identifier,
                MerchantId = paymentResponse.MerchantId,
                PayerCardNum = paymentResponse.PayerCardNum,
                Status = paymentResponse.Status
            };

            return paymentRepository.SaveTransactionDetails(currentTransaction);

        }


    }
}
