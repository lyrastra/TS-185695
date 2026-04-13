using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BankIntegrationsV2.Dto.FrameInfo;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BankIntegrationsV2.Client.FrameInfo
{
    [InjectAsSingleton]
    public class RequisitesFrameInfoClient : BaseApiClient, IBankIntegrationsFrameInfoClient
    {
        private const string ControllerName = "/BankIntegrationsFrameInfo";
        private readonly SettingValue apiEndPoint;

        public RequisitesFrameInfoClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("IntegrationApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerName;
        }

        public Task<BankIntegrationsFrameInfoForFirmResponseDto> GetForFirmByIdAsync(int id)
        {
            return GetAsync<BankIntegrationsFrameInfoForFirmResponseDto>("/GetForFirmById", new { id });
        }

        public Task<BankIntegrationsFrameInfoForFirmResponseDto> GetForFirmByInnAsync(string inn)
        {
            return GetAsync<BankIntegrationsFrameInfoForFirmResponseDto>("/GetForFirmByInn", new { inn });
        }

        public Task<List<BankIntegrationsFrameInfoForFirmResponseDto>> GetForFirmsByIdsAsync(List<int> ids)
        {
            return PostAsync<List<int>, List<BankIntegrationsFrameInfoForFirmResponseDto>>("/GetForFirmsByIds", ids);
        }
    }
}