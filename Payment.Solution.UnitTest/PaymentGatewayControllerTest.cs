using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit.Sdk;
using Moq;
using Payment.Gateway.Domain.Payment;
using Payment.Gateway.Service;
using Payment.Solution.ConfigurationItems;
using Payment.Solution.Controllers;
using System;

namespace Payment.Solution.UnitTest
{
    [TestClass]
    public class PaymentGatewayControllerTest
    {
        [TestMethod]
        public void TestProcessTransactionPass()
        {
            Mock<IPaymentService> mock = new Mock<IPaymentService>();

            Guid merchantId = Guid.NewGuid();
            string payerCardNum = "456712341234";
            string merchantCard = "41231425679";

            PaymentTransactionDetails ptd = new PaymentTransactionDetails
            {
                Amount = 121.0,
                CCV = "231",
                ExpiryDate = "12/12/2020",
                MerchantId = merchantId,
                PayerCardNum = payerCardNum
            };

            PaymentResponse pr = new PaymentResponse
            {
                Amount = 121.0,
                Identifier = Guid.NewGuid(),
                MerchantId = merchantId,
                PayerCardNum = "",
                Status = false
            };

            mock.Setup(i => i.ProcessTransaction(ptd, merchantCard)).Returns(pr);

            MerchantsList ml = new MerchantsList
            {
                MerchantsInfoList = new System.Collections.Generic.List<MerchantInfo>
                {
                    new MerchantInfo
                    {
                        Uid = pr.MerchantId,
                        CardNumber = merchantCard
                    }
                }
            };

           // PaymentGatewayController paymentGatewayController = new PaymentGatewayController(mock.Object, ml);
            //paymentGatewayController.MakePayment(ptd);

            mock.Verify(f => f.ProcessTransaction(ptd, merchantCard));
            Assert.AreEqual(pr, mock.Object.ProcessTransaction(ptd, merchantCard));
        }

    }
}
