using Moedelo.Common.Enums.Enums.RegistrationService;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.Registration.Dto;
using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Registration.Client
{
    [InjectAsSingleton]
    public class RegistrationClient : BaseApiClient, IRegistrationClient
    {
        private readonly SettingValue apiEndPoint;

        public RegistrationClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("RegistrationServiceUrl");
        }

        protected override string GetApiEndpoint()
        {
            return  apiEndPoint.Value + "/Rest/Registration";
        }

        public Task<bool> IsLoginBusyAsync(string login)
        {
            return GetAsync<bool>("/IsLoginBusy", new { login });
        }

        public Task<bool> IsLoginValidAsync(string login, CancellationToken cancellationToken)
        {
            return GetAsync<bool>("/IsLoginValid", new { login }, cancellationToken: cancellationToken);
        }

        public Task<LoginStatus> GetLoginStatusAsync(string login, CancellationToken cancellationToken = default)
        {
            return GetAsync<LoginStatus>("/GetLoginStatus", new { login }, cancellationToken: cancellationToken);
        }

        public Task<RegistrationResultDto> RegistrationUserFromPartnerAsync(RegistrationFromPartnerUserInfoDto userInfo)
        {
            return PostAsync<RegistrationFromPartnerUserInfoDto, RegistrationResultDto>("/RegistrationUserFromPartner", userInfo, setting: new HttpQuerySetting(TimeSpan.FromSeconds(120)));
        }

        public Task<RegistrationResultDto> RegistrationAsync(RegistrationRequestDto request)
        {
            return PostAsync<RegistrationRequestDto, RegistrationResultDto>("/Registration", request, setting: new HttpQuerySetting(TimeSpan.FromSeconds(120)));
        }

        public Task<RegistrationResultDto> RegisterSsoAsync(RegisterSsoRequestDto request)
        {
            return PostAsync<RegisterSsoRequestDto, RegistrationResultDto>("/RegisterSso", request, setting: new HttpQuerySetting(TimeSpan.FromSeconds(120)));
        }

        public Task<GetAuthenticationInfoDto> GetAuthenticationInfoAsync(Guid registrationKey)
        {
            return GetAsync<GetAuthenticationInfoDto>("/GetAuthenticationInfo", new { registrationKey });
        }

        public Task<RegistrationResultDto> RegistrationWithoutPhoneValidationAsync(RegistrationRequestDto request)
        {
            return PostAsync<RegistrationRequestDto, RegistrationResultDto>("/RegistrationWithoutPhoneValidation", request);
        }

        public Task<RegistrationResultDto> RegistrationFromBankEdsWizard(RegistrationFromBankEdsWizardRequest request)
        {
            var settings = new HttpQuerySetting(TimeSpan.FromSeconds(180));
            
            return PostAsync<RegistrationFromBankEdsWizardRequest, RegistrationResultDto>("/RegistrationFromBankEdsWizard", request, null, settings);
        }
    }
}
