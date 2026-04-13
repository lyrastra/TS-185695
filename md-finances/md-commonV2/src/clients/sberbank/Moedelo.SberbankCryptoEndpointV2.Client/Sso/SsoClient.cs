using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SberbankCryptoEndpointV2.Dto.Sso;

namespace Moedelo.SberbankCryptoEndpointV2.Client.Sso
{
    [InjectAsSingleton]
    public class SsoClient : BaseApiClient, ISsoClient
    {
        private readonly SettingValue apiEndpoint;

        public SsoClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("SberbankCryptoEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/SsoV2/";
        }
        
        public Task<bool> UpdateIntegrationDataAsync(IntegrationDataDto dto)
        {
            return PostAsync<IntegrationDataDto, bool>("UpdateIntegrationData", dto);
        }
        
        public Task<AccessTokenResponseDto> GetAccessTokenV2Async(AccessTokenRequestDto dto)
        {
            return PostAsync<AccessTokenRequestDto, AccessTokenResponseDto>("GetAccessTokenV2", dto);
        }

        public Task<UserInfoResponseDto> GetUserInfoV2Async(UserInfoRequestDto dto)
        {
            return PostAsync<UserInfoRequestDto, UserInfoResponseDto>("GetUserInfoV2", dto);
        }
    }
}