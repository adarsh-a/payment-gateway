﻿using System;
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
using Moq;

namespace Payment.Solution.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PaymentGatewayController : Controller
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
        [Route("makepayment")]
        public string MakePayment([FromBody] PaymentTransactionDetails paymentTransactionDetails)
        {
            var msg = string.Empty;
            if (merchantsList != null && merchantsList.MerchantsInfoList != null && merchantsList.MerchantsInfoList.Any())
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
                            if(transactionResponse != null && transactionResponse.Identifier!= Guid.Empty) 
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
                msg = "Merchants list not available";
            }

            return msg;
        }


        [HttpPost]
        [Route("gettransactiondetails")]
        public IActionResult GetTransactionDetails([FromBody] string merchantId)
        {
            PaymentLogResponse lresponse = new PaymentLogResponse();
            lresponse.PaymentHistory = new List<Gateway.Domain.Logs.LogDetails>();

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

            return Json(lresponse);

        }

        [HttpGet]
        public ActionResult<string> Welcome()
        {
            return "Welcome to the Payment Gateway";
        }
    }
}