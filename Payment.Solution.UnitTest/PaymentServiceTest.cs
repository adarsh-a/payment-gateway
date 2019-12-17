using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Payment.Gateway.Domain.CardManagement;
using Payment.Gateway.Domain.Payment;
using Payment.Gateway.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Solution.UnitTest
{
    [TestClass]
    public class PaymentServiceTest
    {
        [TestMethod]
        public void TestCardInValidNumber()
        {
            Guid merchantId = Guid.NewGuid();
            string payerCardNum = "456712341234";

            PaymentTransactionDetails paymentTransactionDetails = new PaymentTransactionDetails
            {
                Amount = 121.0,
                CCV = "231",
                ExpiryDate = "12/12/2020",
                MerchantId = merchantId,
                PayerCardNum = payerCardNum
            };

            Mock<IPaymentService> mock = new Mock<IPaymentService>();


            Card outputCard = new Card
            {
                CardNum = payerCardNum,
                CCV = "231",
                ExpiryDate = "12/12/2020"

            };

            mock.Setup(i => i.CheckCard(paymentTransactionDetails)).Returns(outputCard);

            mock.Object.CheckCard(paymentTransactionDetails);

            mock.Verify(i => i.CheckCard(paymentTransactionDetails));

            Assert.AreEqual(false, mock.Object.CheckCard(paymentTransactionDetails).IsValid);
            
        }

        [TestMethod]
        public void TestCardExpired()
        {
            Guid merchantId = Guid.NewGuid();
            string payerCardNum = "6031645798969426";

            PaymentTransactionDetails paymentTransactionDetails = new PaymentTransactionDetails
            {
                Amount = 121.0,
                CCV = "231",
                ExpiryDate = "12/12/2019",
                MerchantId = merchantId,
                PayerCardNum = payerCardNum
            };

            Mock<IPaymentService> mock = new Mock<IPaymentService>();


            Card outputCard = new Card
            {
                CardNum = payerCardNum,
                CCV = "231",
                ExpiryDate = "12/12/2019"

            };

            mock.Setup(i => i.CheckCard(paymentTransactionDetails)).Returns(outputCard);

            mock.Object.CheckCard(paymentTransactionDetails);

            mock.Verify(i => i.CheckCard(paymentTransactionDetails));

            Assert.AreEqual(false, mock.Object.CheckCard(paymentTransactionDetails).IsValid);


        }

        [TestMethod]
        public void TestCardValid()
        {
            Guid merchantId = Guid.NewGuid();
            string payerCardNum = "6031645798969426";

            PaymentTransactionDetails paymentTransactionDetails = new PaymentTransactionDetails
            {
                Amount = 121.0,
                CCV = "231",
                ExpiryDate = "12/12/2021",
                MerchantId = merchantId,
                PayerCardNum = payerCardNum
            };

            Mock<IPaymentService> mock = new Mock<IPaymentService>();


            Card outputCard = new Card
            {
                CardNum = payerCardNum,
                CCV = "231",
                ExpiryDate = "12/12/2021"

            };

            mock.Setup(i => i.CheckCard(paymentTransactionDetails)).Returns(outputCard);

            mock.Object.CheckCard(paymentTransactionDetails);

            mock.Verify(i => i.CheckCard(paymentTransactionDetails));

            Assert.AreEqual(false, mock.Object.CheckCard(paymentTransactionDetails).IsValid);


        }
    }
}
