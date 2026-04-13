using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Docs.Client.Docs
{
    [InjectAsSingleton]
    public class DocsKontragentsApiClient : BaseApiClient, IDocsKontragentsApiClient
    {
        private readonly SettingValue apiEndpoint;

        public DocsKontragentsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("DocsApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<List<int>> GetUsedInDocsAsync(int firmId, int userId, IReadOnlyCollection<int> kontragentIds)
        {
            if (kontragentIds == null || !kontragentIds.Any())
            {
                return Task.FromResult(new List<int>());
            }
            
            return PostAsync<IReadOnlyCollection<int>, List<int>>(
                $"/Kontragents/GetUsedInDocs?firmId={firmId}&userId={userId}", 
                kontragentIds);
        }
    }
}