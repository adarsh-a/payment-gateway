using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payment.Gateway.Domain.CardManagement
{
    public class Card
    {
        public string CardNum { get; set; }

        public string CCV { get; set; }

        public string ExpiryDate { get; set; }

        private bool Status { get; set; }

        public Card ValidateCard(Card cardDetails)
        {
            if (!string.IsNullOrEmpty(cardDetails.CardNum) && !string.IsNullOrEmpty(cardDetails.CCV) && !string.IsNullOrEmpty(cardDetails.ExpiryDate))
            {
                if (!string.IsNullOrEmpty(cardDetails.CardNum) && cardDetails.CardNum.Length == 16)
                {
                    cardDetails.Status = Luhn(cardDetails.CardNum);
                    if (cardDetails.Status)
                    {
                        cardDetails.Status = CheckExpiryDate(cardDetails.ExpiryDate);


                    }
                }
            }
            return cardDetails;
        }

        public bool IsValid
        {
            get
            {
                return this.Status;
            }
        }


        private bool CheckExpiryDate(string expiryDate)
        {
            DateTime cardExpiry = new DateTime();
            if (DateTime.TryParse(expiryDate, out cardExpiry))
            {
                if (cardExpiry > DateTime.Now)
                {
                    return true;
                }
            }
            return false;
        }

        private bool Luhn(string digits)
        {
            return digits.All(char.IsDigit) && digits.Reverse()
                .Select(c => c - 48)
                .Select((thisNum, i) => i % 2 == 0
                    ? thisNum
                    : ((thisNum *= 2) > 9 ? thisNum - 9 : thisNum)
                ).Sum() % 10 == 0;
        }
    }
}
