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
using Moedelo.Payroll.ApiClient.Abstractions.Ndfl2IncomeStatement;
using Moedelo.Payroll.ApiClient.Abstractions.Ndfl2IncomeStatement.Dto;
using Moedelo.Payroll.ApiClient.Legacy;

namespace Moedelo.Payroll.ApiClient.Ndfl2IncomeStatement
{
    [InjectAsSingleton(typeof(INdfl2IncomeStatementApiClient))]
    internal sealed class Ndfl2IncomeStatementApiClient : BaseLegacyApiClient, INdfl2IncomeStatementApiClient
    {
        private static readonly HttpQuerySetting DefaultHttpSetting = new HttpQuerySetting(TimeSpan.FromSeconds(90));

        public Ndfl2IncomeStatementApiClient(
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

        public Task<IReadOnlyCollection<Ndfl2IncomeStatementDataDto>> GetDataAsync(FirmId firmId, UserId userId, 
            Ndfl2IncomeStatementRequestDto request)
        {
            return PostAsync<Ndfl2IncomeStatementRequestDto, IReadOnlyCollection<Ndfl2IncomeStatementDataDto>>(
                $"/Ndfl2IncomeStatement/GetData?firmId={firmId}&userId={userId}", request, setting: DefaultHttpSetting);
        }
    }
}
