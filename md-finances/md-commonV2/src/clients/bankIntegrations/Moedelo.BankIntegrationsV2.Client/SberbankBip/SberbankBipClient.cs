using Moedelo.BankIntegrationsV2.Client.Abstraction.SberbankBip;
using System;
using System.Threading.Tasks;
using Moedelo.BankIntegrationsV2.Dto.BankOperation;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.BankIntegrationsV2.Client.SberbankBip
{
    [InjectAsSingleton]
    public class SberbankBipClient : BaseCoreApiClient, ISberbankBipClient
    {
        private readonly SettingValue apiEndPoint;
        private const string ControllerName = "/SberbankBip/";

        public SberbankBipClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
                : base(httpRequestExecutor, uriCreator, responseParser, settingRepository, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("IntegrationApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerName;
        }

        public Task<bool> LandingAccessDeniedForBipAsync(string externalClientId)
        {
            return GetAsync<bool>($"LandingAccessDeniedForBip?externalClientId={externalClientId}");
        }

        public Task<SberbankBipResponseDto> MovingBipToMyDealAsync(int firmId, int userId)
        {
            return GetAsync<SberbankBipResponseDto>($"MovingBipToMyDeal?firmId={firmId}&userId={userId}");
        }
    }
}
