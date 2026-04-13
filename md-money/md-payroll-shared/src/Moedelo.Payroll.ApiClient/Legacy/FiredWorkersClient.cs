using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.FiredWorkers;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IFiredWorkersClient))]
    internal sealed class FiredWorkersClient : BaseLegacyApiClient, IFiredWorkersClient
    {
        public FiredWorkersClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<FiredWorkersClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"),
                logger)
        {
        }

        public Task UpdateOrderAsync(int firmId, int userId, FiredWorkerOrderUpdatingDto dto)
        {
            return PostAsync($"/FiredWorkers/UpdateOrder?firmId={firmId}&userId={userId}", dto);
        }
    }

   
}
