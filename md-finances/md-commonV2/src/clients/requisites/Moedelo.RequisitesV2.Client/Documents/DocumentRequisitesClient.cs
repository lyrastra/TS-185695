using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RequisitesV2.Dto;
using Moedelo.RequisitesV2.Dto.Documents;

namespace Moedelo.RequisitesV2.Client.Documents
{
    [InjectAsSingleton]
    public class DocumentRequisitesClient : BaseApiClient, IDocumentRequisitesClient
    {
        private readonly SettingValue apiEndPoint;
        
        public DocumentRequisitesClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, 
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager
            )
        {
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
        
        public async Task<DocumentRequisitesDto> GetAsync(int firmId, int userId)
        {
            var result = await GetAsync<ApiDataResult<DocumentRequisitesDto>>(
                "/DocumentRequisites/Get", 
                new { firmId, userId }).ConfigureAwait(false);
            return result.Data;
        }

        public Task SetIsBillQrCodeEnabled(int firmId, int userId, bool isBillQrCodeEnabled)
        {
            return PostAsync($"/DocumentRequisites/SetIsBillQrCodeEnabled?firmId={firmId}&userId={userId}&isBillQrCodeEnabled={isBillQrCodeEnabled}");
        }
    }
}