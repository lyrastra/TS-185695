using System.Threading.Tasks;
using Moedelo.Authorization.Client.Abstractions;
using Moedelo.Authorization.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Authorization.Client
{
    [InjectAsSingleton(typeof(IAuthorizationContextApiClient))]
    public class AuthorizationContextApiClient : BaseCoreApiClient, IAuthorizationContextApiClient
    {
        private readonly SettingValue endpoint;
        
        public AuthorizationContextApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            this.endpoint = settingRepository.GetRequired("AuthorizationPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return endpoint.Value;
        }

        public Task<AuthorizationContextDto> GetAsync(int firmId, int userId)
        {
            return GetAsync<AuthorizationContextDto>("/v1/AuthorizationContext", new {firmId, userId});
        }
    }
}