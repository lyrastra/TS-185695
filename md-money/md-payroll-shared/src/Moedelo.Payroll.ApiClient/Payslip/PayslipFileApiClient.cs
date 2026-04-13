using System;
using System.Threading;
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
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.File;
using Moedelo.Payroll.ApiClient.Abstractions.Payslip;

namespace Moedelo.Payroll.ApiClient.Payslip
{
    [InjectAsSingleton(typeof(IPayslipFileApiClient))]
    public class PayslipFileApiClient : BaseLegacyApiClient, IPayslipFileApiClient
    {
        private static readonly HttpQuerySetting DefaultHttpSetting = new HttpQuerySetting(TimeSpan.FromSeconds(90));

        public PayslipFileApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PayslipFileApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"),
                logger)
        {
        }

        public Task<FileResultDto> GetFileAsync(FirmId firmId, UserId userId, int year, int month, int workerId,
            CancellationToken token = default)
        {
            return GetAsync<FileResultDto>(
                uri: "/PayslipFile",
                queryParams: new
                {
                    FirmId = firmId, UserId = userId, Year = year, Month = month, WorkerId = workerId
                },
                setting: DefaultHttpSetting,
                cancellationToken: token);
        }
    }
}