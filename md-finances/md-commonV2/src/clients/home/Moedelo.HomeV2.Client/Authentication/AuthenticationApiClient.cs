using System.Threading.Tasks;
using Moedelo.HomeV2.Dto.Authentication;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HomeV2.Client.Authentication
{
    [InjectAsSingleton]
    public class AuthenticationApiClient : BaseApiClient, IAuthenticationApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public AuthenticationApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("HomePrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Rest/Authentication";
        }

        public async Task<UserVerificationResponseDto> VerifyAsync(int firmId, int userId, UserVerificationRequestDto dto)
        {
            var response =  await PostAsync<UserVerificationRequestDto, DataRequestWrapper<UserVerificationResponseDto>>($"/Verify?firmId={firmId}&userId={userId}", dto).ConfigureAwait(false);
            return response.Data;
        }
    }
}