using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Parsers.Klto1CParser.Enums;
using Moedelo.Parsers.Klto1CParser.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Finances.Business.Services.Money.Reconciliation.ForUser
{
    [InjectAsSingleton]
    public class DocumentTypeApiClient : BaseApiClient, IDocumentTypeApiClient
    {
        private readonly SettingValue apiEndpoint;

        public DocumentTypeApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("PaymentImportApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<IDictionary<string, TransferType>> DetermineAsync(int firmId, int userId, string settlementNumber, IReadOnlyCollection<Document> documents)
        {
            return PostAsync<IReadOnlyCollection<Document>, IDictionary<string, TransferType>>($"/DocumentType/Determine?firmId={firmId}&userId={userId}&settlementNumber={settlementNumber}", documents);
        }
    }
}
