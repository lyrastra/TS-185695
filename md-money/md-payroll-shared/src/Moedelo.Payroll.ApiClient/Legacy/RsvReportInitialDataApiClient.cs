using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.RsvReportInitialData;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IRsvReportInitialDataApiClient))]
    public class RsvReportInitialDataApiClient : BaseLegacyApiClient, IRsvReportInitialDataApiClient
    {
        private static readonly HttpQuerySetting DefaultHttpSetting = new HttpQuerySetting(TimeSpan.FromSeconds(90));
        
        public RsvReportInitialDataApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PsvReportApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"), 
                logger)
        {
        }

        public Task<RsvReportInitialDataDto> GetInitialDataAsync(FirmId firmId, UserId userId, int year, int periodNumber)
        {
            return GetAsync<RsvReportInitialDataDto>("/RsvReportInitialData/GetInitialData", new { firmId, userId, year, periodNumber },
                setting: DefaultHttpSetting);
        }

    }
}