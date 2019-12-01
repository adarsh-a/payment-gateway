using Payment.Gateway.Domain.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Solution.Models
{
    public class PaymentLogResponse : ResponseBase
    {
        public List<LogDetails> PaymentHistory { get; set; }
    }
}
