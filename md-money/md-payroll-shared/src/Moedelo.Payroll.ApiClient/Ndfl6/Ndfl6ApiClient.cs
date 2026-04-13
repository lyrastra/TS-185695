using System;
using System.Collections.Generic;
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
using Moedelo.Payroll.ApiClient.Abstractions.Ndfl6;
using Moedelo.Payroll.ApiClient.Abstractions.Ndfl6.Dto;
using Moedelo.Payroll.ApiClient.Legacy;

namespace Moedelo.Payroll.ApiClient.Ndfl6
{
    [InjectAsSingleton(typeof(INdfl6ApiClient))]
    internal sealed class Ndfl6ApiClient : BaseLegacyApiClient, INdfl6ApiClient
    {
        private static readonly HttpQuerySetting DefaultHttpSetting = new HttpQuerySetting(TimeSpan.FromSeconds(90));

        public Ndfl6ApiClient(
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

        public Task<Ndfl6DataDto> GetDataAsync(FirmId firmId, UserId userId, int year, int quarter,
            int ndflSettingId)
        {
            return GetAsync<Ndfl6DataDto>("/Ndfl6/GetData", new { firmId, userId, year, quarter, ndflSettingId },
                setting: DefaultHttpSetting);
        }
        
        public Task<IReadOnlyCollection<Ndfl6NdflSettingDto>> GetNdflSettingsAsync(FirmId firmId, UserId userId, 
            int year, int quarter)
        {
            return GetAsync<IReadOnlyCollection<Ndfl6NdflSettingDto>>("/Ndfl6/GetNdflSettings", new { firmId, userId, 
                year, quarter }, setting: DefaultHttpSetting);
        }
    }
}
