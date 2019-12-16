using Payment.Log.Constants;
using Serilog;
using Serilog.Debugging;
using Serilog.Sinks.Elasticsearch;
using System;
using static Payment.Log.Constants.ErrorSet;

namespace Payment.Log.Service
{
    public class Logger : ILogger
    {
        public Logger()
        {
            SelfLog.Enable(Console.Error);
            Serilog.Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.Console()
                               .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                               {
                                   AutoRegisterTemplate = false,
                                   AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6

                               })
                   .CreateLogger();
        }

        public void InsertLog(string msg, Errors errorType)
        {
            if (errorType == Errors.Info)
            {
                Serilog.Log.Information(msg);
            }
            if (errorType == Errors.Error)
            {
                Serilog.Log.Fatal(msg);
            }
        }
    }
}
