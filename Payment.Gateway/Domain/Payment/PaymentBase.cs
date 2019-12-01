using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Gateway.Domain.Payment
{
    [JsonObject]
    public class PaymentBase
    {
        [JsonProperty("merchantid")]
        public Guid MerchantId { get; set; }

        [JsonProperty("payercardnum")]
        public string PayerCardNum { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }
    }
}
