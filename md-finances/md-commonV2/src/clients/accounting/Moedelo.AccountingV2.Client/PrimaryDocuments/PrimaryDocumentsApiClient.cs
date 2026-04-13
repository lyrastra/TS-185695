using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.PrimaryDocuments;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.PrimaryDocuments
{
    [InjectAsSingleton]
    public class PrimaryDocumentsApiClient : BaseApiClient, IPrimaryDocumentsApiClient
    {
        private readonly SettingValue apiEndPoint;

        public PrimaryDocumentsApiClient(
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

        public Task ReplaceKontragentInPrimaryDocumentsAsync(int firmId, int userId, KontragentReplaceDto request)
        {
            return PostAsync($"/PrimaryDocumentsApi/ReplaceKontragentInDocuments?firmId={firmId}&userId={userId}", request);
        }

        public Task DeleteNotBindedToBalancesAsync(int firmId, int userId)
        {
            return PostAsync($"/PrimaryDocumentsApi/DeleteNotBindedToBalances?firmId={firmId}&userId={userId}"); 
        }
    }
}
