using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.Docs.Dto.Docs;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Docs.Client.Upd
{
    [InjectAsSingleton]
    public class UpdProductUnionApiClient : BaseApiClient, IUpdProductUnionApiClient
    {
        private readonly SettingValue apiEndpoint;

        public UpdProductUnionApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("DocsApiEndpoint");
        }

        public Task UnionProductAsync(int firmId, int userId, ProductUnionDto productUnionModel)
        {
            return PostAsync($"/UnionProducts?firmId={firmId}&userId={userId}", productUnionModel);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }
    }
}