using System.Threading.Tasks;
using Moedelo.HomeV2.Dto.Social;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HomeV2.Client.Social
{
    [InjectAsSingleton]
    public class SocialApiClient : BaseApiClient, ISocialApiClient
    {
        private const string ControllerUri = "/Rest/Social";

        private readonly SettingValue apiEndPoint;

        public SocialApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint= settingRepository.Get("HomePrivateApiEndpoint");
        }

        public Task SaveUserSocialInfoAsync(UserSocialInfoDto socialInfo)
        {
            return PostAsync<UserSocialInfoDto>("/SaveUserSocialInfo", socialInfo);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerUri;
        }
    }
}