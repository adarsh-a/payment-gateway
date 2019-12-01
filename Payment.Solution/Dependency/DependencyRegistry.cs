using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Payment.Gateway.Repository;
using Payment.Gateway.Service;
using Payment.Solution.ConfigurationItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Solution.Dependency
{
    public class DependencyRegistry : IDependency
    {
        public void Register(IServiceCollection services)
        {
            #region settings
            services.AddScoped(configuration => configuration.GetService<IOptionsSnapshot<MerchantsList>>().Value);

            #endregion

            #region Services

            services.AddSingleton<IPaymentService, PaymentService>();
            services.AddSingleton<IPaymentDetailsRepository, PaymentDetailsRepository>();



            #endregion
        }
    }
}
