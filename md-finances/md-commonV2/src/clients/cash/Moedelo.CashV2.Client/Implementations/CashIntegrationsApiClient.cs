using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CashV2.Client.Contracts;
using Moedelo.CashV2.Dto.Cashbox;
using Moedelo.CashV2.Dto.YandexKassa.IntegrationTurn;
using Moedelo.CashV2.Dto.YandexKassa.PaymentImport;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CashV2.Client.Implementations
{
    [InjectAsSingleton]
    public class CashIntegrationsApiClient : BaseApiClient, ICashIntegrationsApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public CashIntegrationsApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,  
            ISettingRepository settingRepository)
            : base(
                  httpRequestExecutor,
                  uriCreator, 
                  responseParser, auditTracer, auditScopeManager
                  )
        {
            apiEndPoint = settingRepository.Get("CashPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<List<CashboxDto>> GetList(int firmId)
        {
            return GetAsync<List<CashboxDto>>($"/api/v1/cashbox?firmId={firmId}");
        }

        public Task<List<MdMovementListDto>> GetIntegrations(int firmId, int userId)
        {
            return GetAsync<List<MdMovementListDto>>($"/Integrations/GetIntegrations?firmId={firmId}&userId={userId}");
        }

        public Task<bool> Skip(int firmId, int userId, int integratedFileId)
        {
            return PostAsync<bool>($"/Integrations/SkipIntegrations?firmId={firmId}&userId={userId}&integratedFileId={integratedFileId}");
        }

        public Task<bool> Import(int firmId, int userId, int integratedFileId)
        {
            return PostAsync<bool>($"/Integrations/ImportIntegrations?firmId={firmId}&userId={userId}&integratedFileId={integratedFileId}");
        }

        public Task<bool> IntegrationTurnAsync(int firmId, int userId, CashIntegrationTurnRequestDto dto)
        {
            return PostAsync<CashIntegrationTurnRequestDto, bool>($"/Integrations/IntegrationTurnAsync/?firmId={firmId}&userId={userId}", dto);
        }
    }
}
