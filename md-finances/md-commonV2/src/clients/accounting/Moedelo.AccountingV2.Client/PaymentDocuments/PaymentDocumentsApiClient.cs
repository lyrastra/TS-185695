using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.PaymentDocuments;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.PaymentDocuments
{
    [InjectAsSingleton]
    public class PaymentDocumentsApiClient : BaseApiClient, IPaymentDocumentsApiClient
    {
        private readonly SettingValue apiEndPoint;

        public PaymentDocumentsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<IncomingOutgoingSumDto> GetKontragentIncomingAndOutgoingOperationsSumAsync(int firmId, int userId, int kontragentId)
        {
            var result = await GetAsync<DataResponseWrapper<IncomingOutgoingSumDto>>(
                $"/PaymentDocumentApi/GetKontragentIncomingAndOutgoingOperationsSum?firmId={firmId}&userId={userId}&kontragentId={kontragentId}")
                .ConfigureAwait(false);
            return result.Data;
        }

        public async Task<List<IncomingOutgoingSumDto>> GetKontragentIncomingAndOutgoingOperationsSumListAsync(int firmId, int userId, IReadOnlyCollection<int> kontragentIds)
        {
            var param = new { kontragentIds };
            var result = await PostAsync<object, ListResponseWrapper<IncomingOutgoingSumDto>>(
                $"/PaymentDocumentApi/GetKontragentIncomingAndOutgoingOperationsSumList?firmId={firmId}&userId={userId}", param)
                .ConfigureAwait(false);
            return result.Items;
        }

        public Task ReplaceKontragentInPaymentDocumentsAsync(int firmId, int userId, KontragentReplaceDto request)
        {
            return PostAsync($"/PaymentDocumentApi/ReplaceKontragentInPaymentDocuments?firmId={firmId}&userId={userId}", request);
        }
    }
}
