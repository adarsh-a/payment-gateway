using System;
using System.Collections.Generic;
using System.Text;
using static Payment.Log.Constants.ErrorSet;

namespace Payment.Log.Service
{
    public interface ILogger
    {
        void InsertLog(string msg, Errors errorType);
    }
}
