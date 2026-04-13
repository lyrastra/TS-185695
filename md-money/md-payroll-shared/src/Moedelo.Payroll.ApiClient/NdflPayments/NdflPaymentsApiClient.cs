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
using Moedelo.Payroll.ApiClient.Abstractions.NdflPayments;
using Moedelo.Payroll.ApiClient.Abstractions.NdflPayments.Dto;
using Moedelo.Payroll.ApiClient.Legacy;

namespace Moedelo.Payroll.ApiClient.NdflPayments
{
    [InjectAsSingleton(typeof(INdflPaymentsApiClient))]
    internal sealed class NdflPaymentsApiClient : BaseLegacyApiClient, INdflPaymentsApiClient
    {
        private static readonly HttpQuerySetting DefaultHttpSetting = new HttpQuerySetting(TimeSpan.FromSeconds(90));

        public NdflPaymentsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<Ndfl6ReportInitialDataApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"), 
                logger)
        {
        }

        public Task<NdflPaymentsDataDto> GetDataAsync(FirmId firmId, UserId userId, DateTime startDate, DateTime endDate)
        {
            return GetAsync<NdflPaymentsDataDto>("/NdflPayments/GetData",
                new {firmId, userId, startDate, endDate}, setting: DefaultHttpSetting);
        }
    }
}
