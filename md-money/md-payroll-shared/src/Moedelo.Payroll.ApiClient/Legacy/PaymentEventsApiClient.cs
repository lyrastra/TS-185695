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
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentEvents;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PaymentEvents.GenerateEventFiles;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IPaymentEventsApiClient))]
    public class PaymentEventsApiClient : BaseLegacyApiClient, IPaymentEventsApiClient
    {
        private static readonly HttpQuerySetting DefaultHttpSetting = new HttpQuerySetting(TimeSpan.FromMinutes(2));

        public PaymentEventsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PaymentEventsApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"),
                logger)
        {
        }

        public Task<PaymentEventInitialDataResponseDto> GetInitialDataAsync(FirmId firmId, UserId userId, PaymentEventInitialDataRequestDto request)
        {
            return PostAsync<PaymentEventInitialDataRequestDto, PaymentEventInitialDataResponseDto>(
                $"/PaymentEvents/GetInitialData?firmId={firmId}&userId={userId}", request, setting: DefaultHttpSetting);
        }

        public Task<IReadOnlyList<EventFilesGroupDto>> GenerateEventFilesAsync(FirmId firmId, UserId userId, EventFilesRequestDto request)
        {
            return PostAsync<EventFilesRequestDto, IReadOnlyList<EventFilesGroupDto>>(
                $"/PaymentEvents/GenerateEventFiles?firmId={firmId}&userId={userId}", request, setting: DefaultHttpSetting);
        }
    }
}