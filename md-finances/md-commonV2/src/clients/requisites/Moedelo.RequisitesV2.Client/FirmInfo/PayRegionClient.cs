using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RequisitesV2.Dto.FirmInfo;

namespace Moedelo.RequisitesV2.Client.FirmInfo
{
    [InjectAsSingleton]
    public class PayRegionClient : BaseApiClient, IPayRegionClient
    {
        private readonly SettingValue apiEndPoint;
        
        public PayRegionClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<FirmPayRegionDto> GetPayRegionByFirmIdAsync(int firmId)
        {
            return GetAsync<FirmPayRegionDto>($"/PayRegion/ByFirmId?firmId={firmId}");
        }

        public Task SaveAsync(int firmId, int regionId)
        {
            return PostAsync($"/PayRegion/Save?firmId={firmId}&regionId={regionId}");
        }
    }
}