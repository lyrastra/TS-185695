using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Ndfl6ReportInitialData;

namespace Moedelo.Payroll.ApiClient.Legacy
{
#pragma warning disable CS0618
    [InjectAsSingleton(typeof(INdfl6ReportInitialDataApiClient))]
    internal sealed class Ndfl6ReportInitialDataApiClient : BaseLegacyApiClient, INdfl6ReportInitialDataApiClient
#pragma warning restore CS0618
    {
        private static readonly HttpQuerySetting DefaultHttpSetting = new HttpQuerySetting(TimeSpan.FromSeconds(90));

        public Ndfl6ReportInitialDataApiClient(
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

        public Task<Ndfl6ReportInitialDataDto> GetInitialDataAsync(int firmId, int userId, int year, int periodNumber,
            int ndflSettingId)
        {
            return GetAsync<Ndfl6ReportInitialDataDto>("/Ndfl6ReportInitialData/GetInitialData",
                new {firmId, userId, year, periodNumber, ndflSettingId}, setting: DefaultHttpSetting);
        }

        public Task<List<int>> GetNdflSettingListAsync(int firmId, int userId, int year, int periodNumber)
        {
            return GetAsync<List<int>>("/Ndfl6ReportInitialData/GetNdflSettingList",
                new { firmId, userId, year, periodNumber }, setting: DefaultHttpSetting);
        }
    }
}
