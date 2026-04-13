using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.SalaryPayments;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Accounting.ApiClient.legacy
{
    [InjectAsSingleton(typeof(ISalaryPaymentsApiClient))]
    public class SalaryPaymentsApiClient : BaseLegacyApiClient, ISalaryPaymentsApiClient
    {
        private readonly HttpQuerySetting defaultSetting = new HttpQuerySetting(TimeSpan.FromMinutes(5));

        public SalaryPaymentsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SalaryPaymentsApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
        }

        public Task<DocumentsNumbersDto> GetPaymentDocumentLastNumbersAsync(int firmId, int userId, GetLastDocumentsNumbersRequestDto request)
        {
            return PostAsync<GetLastDocumentsNumbersRequestDto, DocumentsNumbersDto>(
                $"/SalaryPayments/GetPaymentDocumentLastNumbers?firmId={firmId}&userId={userId}", request,
                setting: defaultSetting);
        }

        public Task<SavedPaymentsDocumentResultDto> SavePaymentDocumentsAsync(int firmId, int userId, SavingPaymentsModelDto data)
        {
            return PostAsync<SavingPaymentsModelDto, SavedPaymentsDocumentResultDto>(
                    $"/SalaryPayments/SavePaymentDocuments?firmId={firmId}&userId={userId}",
                    data, null, defaultSetting);
        }

        public Task<bool> HasDependenciesByWorkerAsync(int firmId, int userId, int workerId)
        {
            return GetAsync<bool>(
                $"/SalaryPayments/HasDependenciesByWorker?firmId={firmId}&userId={userId}",
                new { firmId, userId, workerId }, null, defaultSetting);
        }
    }
}