using Payment.Gateway.Domain.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Gateway.Domain.Logs
{
    public class LogDetails : PaymentResponse
    {
        public DateTime CreatedDate { get; set; }
    }
}
