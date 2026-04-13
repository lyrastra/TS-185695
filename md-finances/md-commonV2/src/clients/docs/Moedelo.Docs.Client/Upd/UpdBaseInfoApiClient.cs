using System.Threading.Tasks;
using Moedelo.Docs.Dto.Upd;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Docs.Client.Upd
{
    [InjectAsSingleton]
    public class UpdBaseInfoApiClient : BaseApiClient, IUpdBaseInfoApiClient
    {
        private readonly SettingValue apiEndpoint;

        public UpdBaseInfoApiClient(
            IHttpRequestExecutor httpRequestExecutor,
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

        public Task<UpdBaseInfoDto> GetByIdAsync(int firmId, int userId, int id)
        {
            return GetAsync<UpdBaseInfoDto>("/Upd/BaseInfo", new { firmId, userId, id });
        }

        public Task<UpdBaseInfoDto> GetByBaseIdAsync(int firmId, int userId, long documentBaseId)
        {
            return GetAsync<UpdBaseInfoDto>("/Upd/BaseInfo", new { firmId, userId, documentBaseId });
        }
    }
}