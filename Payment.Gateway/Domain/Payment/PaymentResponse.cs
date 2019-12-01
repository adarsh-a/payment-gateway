using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Gateway.Domain.Payment
{
    public class PaymentResponse : PaymentBase
    {
        public Guid Identifier { get; set; }

        public bool Status { get; set; }
    }
}
