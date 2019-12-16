using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payment.Solution.ConfigurationItems;
using Payment.Gateway.Domain.Payment;
using Payment.Solution.Models;
using Payment.Gateway.Service;
//using Payment.Log.Service;
using Moq;
using System.Web.Helpers;

namespace Payment.Solution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentGatewayController : ControllerBase
    {
        private IPaymentService paymentService { get; set; }

        private MerchantsList merchantsList { get; set; }

        public PaymentGatewayController(IPaymentService paymentService, MerchantsList merchantsList)
        {
            this.paymentService = paymentService;
            this.merchantsList = merchantsList;
        }
        //Get api/makepayment
        [HttpPost]
       // [Route("makepayment")]
        [Produces("application/json")]
        public string Post([FromBody] PaymentTransactionDetails paymentTransactionDetails)
        {
            Serilog.Log.Information("Method Process Payment Started");
            //logger.InsertLog("Testing", Log.Constants.ErrorSet.Errors.Info);    
            var msg = string.Empty;
            if (merchantsList != null && merchantsList.MerchantsInfoList != null && merchantsList.MerchantsInfoList.Any())
            {
                var card = paymentService.CheckCard(paymentTransactionDetails);
                if (card.IsValid)
                {

                    if (paymentTransactionDetails.MerchantId != Guid.Empty)
                    {
                        var currentMerchant = merchantsList.MerchantsInfoList.FirstOrDefault(i => i.Uid.Equals(paymentTransactionDetails.MerchantId));
                        if (currentMerchant != null)
                        {
                            var merchantCard = currentMerchant.CardNumber;
                            if (!string.IsNullOrEmpty(merchantCard))
                            {
                                //calling the bank API here
                                var transactionResponse = paymentService.ProcessTransaction(paymentTransactionDetails, merchantCard);
                                if (transactionResponse != null && transactionResponse.Identifier != Guid.Empty)
                                {
                                    if (transactionResponse.Status == false)
                                    {
                                        msg = $"An error occured while processing the transaction. Please try again later";

                                    }

                                    var updateHistoryResponse = paymentService.UpdatePaymentHistory(transactionResponse);
                                }
                                else
                                {
                                    msg = $"An error occured while processing the transaction. Please try again later";
                                }

                            }
                        }
                        else
                        {
                            msg = $"Merchant with ID {paymentTransactionDetails.MerchantId.ToString()} cannot be found";

                        }
                    }
                }
                else 
                {
                    msg = "Card is not valid";
                
                }
            }
            else
            {
                msg = "Merchants list not available";
            }

            return msg;
        }


        [HttpGet("{merchantId}", Name = "Get")]
        //[Route("gettransactiondetails")]
        [Produces("application/json")]
        public PaymentLogResponse Get(string merchantId)
        {
            PaymentLogResponse lresponse = new PaymentLogResponse();
            lresponse.PaymentHistory = new List<Gateway.Domain.Logs.PaymentLogDetails>();

            if (!string.IsNullOrEmpty(merchantId))
            {
                Guid merchandGuid;
                if (Guid.TryParse(merchantId, out merchandGuid))
                {
                    var paymentHistory = paymentService.GetPaymentHistory(merchandGuid);
                    if (paymentHistory != null && paymentHistory.Payments != null && paymentHistory.Payments.Any())
                    {
                        lresponse.Success = true;
                        lresponse.PaymentHistory = paymentHistory.Payments;

                    }
                    else
                    {
                        lresponse.Success = true;
                        lresponse.Error = paymentHistory.Message;

                    }
                }
            }

            return lresponse;

        }
    }
}