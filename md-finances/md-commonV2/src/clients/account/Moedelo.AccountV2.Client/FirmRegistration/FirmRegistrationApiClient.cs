using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.FirmRegistration;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountV2.Client.FirmRegistration
{
    [InjectAsSingleton(typeof(IFirmRegistrationApiClient))]
    internal sealed class FirmRegistrationApiClient : BaseApiClient, IFirmRegistrationApiClient
    {
        private readonly SettingValue apiEndPoint;

        public FirmRegistrationApiClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("FirmRegistrationApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task Save(FirmRegistrationV2Dto firmRegistrationV2Dto)
        {
            return PostAsync("/V2/Save", firmRegistrationV2Dto);
        }
    }
}