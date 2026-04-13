using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.AdvanceInvoice;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.Invoice
{
    [InjectAsSingleton(typeof(IPurchasesAdvanceInvoiceApiClient))]
    public class PurchasesAdvanceInvoiceApiClient : BaseApiClient, IPurchasesAdvanceInvoiceApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public PurchasesAdvanceInvoiceApiClient(
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

        public Task<List<PurchasesAdvanceInvoiceDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds, CancellationToken cancellationToken)
        {
            if (baseIds == null || baseIds.Count == 0)
            {
                return Task.FromResult(new List<PurchasesAdvanceInvoiceDto>());
            }

            var uri = $"/PurchasesInvoice/Advance/GetByBaseIds?firmId={firmId}&userId={userId}";

            return PostAsync<IReadOnlyCollection<long>, List<PurchasesAdvanceInvoiceDto>>(uri, baseIds, cancellationToken: cancellationToken);
        }
    }
}