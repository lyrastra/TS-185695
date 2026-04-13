using System.Threading.Tasks;
using Moedelo.Dss.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Dss.Client.QrProtectionCode
{
    [InjectAsSingleton]
    public class DssQrProtectionCodeClient : BaseApiClient, IDssQrProtectionCodeClient
    {
        private readonly SettingValue apiEndpoint;

        public DssQrProtectionCodeClient(ISettingRepository settingRepository,
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor,
                uriCreator,
                responseParser,
                auditTracer,
                auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("DssApiUrl");
        }

        public Task<QrProtectionCodeVerifyResultDto> SendVerificationCodeAsync(string abnGuid)
        {
            return PostAsync<string, QrProtectionCodeVerifyResultDto>($"/SendVerificationCode", abnGuid);
        }

        public Task<QrProtectionCodeVerifyResultDto> VerifyCodeAsync(string abnGuid, string code)
        {
            return GetAsync<QrProtectionCodeVerifyResultDto>($"/VerifyCode?abnGuid={abnGuid.ToString()}&code={code}");
        }

        protected override string GetApiEndpoint()
        {
            return $"{apiEndpoint.Value}/api/v1/QrProtectionCode";
        }
    }
}