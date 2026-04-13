using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IMrotApiClient))]
    internal sealed class MrotApiClient : BaseLegacyApiClient, IMrotApiClient
    {
        public MrotApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,            
            ISettingRepository settingRepository,
            ILogger<MrotApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"), 
                logger)
        {
        }

        public Task<decimal?> GetRegionalMrotSum(string regionCode, DateTime? date = null)
        {
            return GetAsync<decimal?>("/Mrot/GetRegionalMrotSum", new { regionCode, date });
        }
        
        public Task<decimal?> GetFederalMrotSum(DateTime? date = null)
        {
            return GetAsync<decimal?>("/Mrot/GetFederalMrotSum", new { date });
        }
    }
}
