using System.Threading.Tasks;
using Moedelo.Docs.Dto.SalesUpd;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Docs.Client.SalesUpd
{
    [InjectAsSingleton]
    public class SalesUpdBaseInfoApiClient : BaseApiClient, ISalesUpdBaseInfoApiClient
    {
        private readonly SettingValue apiEndpoint;

        public SalesUpdBaseInfoApiClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) :
            base(
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

        public Task<SalesUpdBaseInfoDto> GetByIdAsync(int firmId, int userId, int id)
        {
            return GetAsync<SalesUpdBaseInfoDto>("/SalesUpd/BaseInfo", new { firmId, userId, id });
        }
    }
}
