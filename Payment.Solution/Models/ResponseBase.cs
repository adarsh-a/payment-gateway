using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Solution.Models
{
    public class ResponseBase
    {
        public bool Success { get; set; }

        public string Error { get; set; }
    }
}
