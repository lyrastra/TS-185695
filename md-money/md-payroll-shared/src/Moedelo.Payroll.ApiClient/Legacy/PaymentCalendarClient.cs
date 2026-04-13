using System;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentCalendar;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IPaymentCalendarClient))]
    internal sealed class PaymentCalendarClient : BaseLegacyApiClient, IPaymentCalendarClient
    {
        private static readonly HttpQuerySetting DefaultHttpSetting = new HttpQuerySetting(TimeSpan.FromMinutes(2));

        public PaymentCalendarClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<WorkerPositionsClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"),
                logger)
        {
        }

        public Task<PaymentCalendarInitialDataDto> GetInitialDataAsync(int firmId, int userId)
        {
            return GetAsync<PaymentCalendarInitialDataDto>(
                $"/PaymentCalendar/InitialData?firmId={firmId}&userId={userId}", setting: DefaultHttpSetting);
        }
    }
}