using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Registration.Dto;

namespace Moedelo.Registration.Client.RegistrationPrivate
{
    [InjectAsSingleton(typeof(IRegistrationPrivateApiClient))]
    internal sealed class RegistrationPrivateApiClient : BaseApiClient, IRegistrationPrivateApiClient
    {
        private readonly SettingValue apiEndPoint;

        public RegistrationPrivateApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator,
                  responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("RegistrationPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Rest/Registration";
        }

        public Task<RegisterForAccountResponseDto> RegisterForAccountAsync(int firmId, int userId,
            RegisterForAccountRequestDto dto)
        {
            return PostAsync<RegisterForAccountRequestDto, RegisterForAccountResponseDto>(
                $"/RegisterForAccount?firmId={firmId}&userId={userId}", dto,
                setting: new HttpQuerySetting(TimeSpan.FromSeconds(120)));
        }
    }
}