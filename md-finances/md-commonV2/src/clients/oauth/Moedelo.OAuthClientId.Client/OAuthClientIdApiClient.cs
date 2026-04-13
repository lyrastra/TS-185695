using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.OAuthClientId.Dto;

namespace Moedelo.OAuthClientId.Client
{
    [InjectAsSingleton(typeof(IOAuthClientIdApiClient))]
    public sealed class OAuthClientIdApiClient : BaseApiClient, IOAuthClientIdApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public OAuthClientIdApiClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("IdentityServerUrl");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<List<OAuthClientIdDto>> GetAllAsync()
        {
            return GetAsync<List<OAuthClientIdDto>>("/api/ClientId/GetAll");
        }
    }
}
