using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Gateway.Domain.Payment
{
    [JsonObject]
    public class PaymentTransactionDetails : PaymentBase
    {
        [JsonProperty("ccv")]
        public string CCV { get; set; }

        [JsonProperty("expirydate")]
        public string ExpiryDate { get; set; }
    }
}
