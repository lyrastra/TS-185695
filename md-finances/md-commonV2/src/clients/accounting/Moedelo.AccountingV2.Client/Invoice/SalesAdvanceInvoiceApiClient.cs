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
    [InjectAsSingleton(typeof(ISalesAdvanceInvoiceApiClient))]
    public class SalesAdvanceInvoiceApiClient : BaseApiClient, ISalesAdvanceInvoiceApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public SalesAdvanceInvoiceApiClient(
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

        public Task<SalesAdvanceInvoiceDto> GetByBaseIdAsync(int firmId, int userId, long baseId)
        {
            return GetAsync<SalesAdvanceInvoiceDto>($"/api/v1/sales/invoice/advance/{baseId}?firmId={firmId}&userId={userId}");
        }

        public Task<List<SalesAdvanceInvoiceDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds, CancellationToken cancellationToken)
        {
            if (baseIds == null || baseIds.Count == 0)
            {
                return Task.FromResult(new List<SalesAdvanceInvoiceDto>());
            }

            var uri = $"/SalesInvoice/Advance/GetByBaseIds?firmId={firmId}&userId={userId}";

            return PostAsync<IReadOnlyCollection<long>, List<SalesAdvanceInvoiceDto>>(uri, baseIds, cancellationToken: cancellationToken);
        }
    }
}
