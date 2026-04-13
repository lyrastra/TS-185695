using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Accounting.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IPeriodStateClient))]
    internal class PeriodStateClient : BaseLegacyApiClient, IPeriodStateClient
    {
        public PeriodStateClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PeriodStateClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
        }

        public Task SaveQuarterAsync(FirmId firmId, UserId userId, int year, int quarter)
        {
            return PostAsync<Task>(
                $"/PeriodState/SaveQuarter?firmId={firmId}&userId={userId}&year={year}&quarter={quarter}",
                null,
                new HttpQuerySetting(TimeSpan.FromMinutes(2))
            );
        }
    }
}
