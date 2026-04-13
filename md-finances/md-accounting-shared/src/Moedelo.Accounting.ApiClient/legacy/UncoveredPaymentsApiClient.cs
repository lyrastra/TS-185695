using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
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
    [InjectAsSingleton(typeof(IUncoveredPaymentsApiClient))]
    internal sealed class UncoveredPaymentsApiClient : BaseLegacyApiClient, IUncoveredPaymentsApiClient
    {
        private static readonly HttpQuerySetting DefaultSettingValue = new HttpQuerySetting(TimeSpan.FromSeconds(30));
        
        public UncoveredPaymentsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<UncoveredPaymentsApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
        }

        public Task<HttpFileModel> DownloadReportAsync(FirmId firmId, UserId userId, UncoveredPaymentsRequestDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var url = new StringBuilder("/UncoveredPayments/DownloadReport?")
                .AppendFormat("?firmId={0}", firmId)
                .AppendFormat("&userId={0}", userId)
                .AppendFormat("&startDate={0:yyyy-MM-dd}", request.StartDate)
                .AppendFormat("&endDate={0:yyyy-MM-dd}", request.EndDate)
                .AppendFormat("&NoUncoveredFileNamePrefix={0}", request.NoUncoveredFileNamePrefix)
                .ToString();
            
            return DownloadFileAsync(url, setting: DefaultSettingValue);
        }

        public Task<bool> HasUncoveredOperationsAsync(FirmId firmId, UserId userId, DateTime startDate, DateTime endDate)
        {
            return GetAsync<bool>($"/UncoveredPayments/HasUncoveredOperations?firmId={(int)firmId}&userId={(int)userId}&startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
        }
    }
}