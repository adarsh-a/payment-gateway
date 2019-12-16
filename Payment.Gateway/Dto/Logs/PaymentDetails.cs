using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Gateway.Domain.Logs
{
    public class PaymentDetails
    {
        public string Message { get; set; }

        public List<PaymentLogDetails> Payments { get; set; }
    }
}
