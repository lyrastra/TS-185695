using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Tools;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Tariffs.Dto.TariffLimits;

namespace Moedelo.Tariffs.Client.TariffLimits
{
    [InjectAsSingleton]
    public class TariffLimitsClient : BaseApiClient, ITariffLimitsClient
    {
        private readonly SettingValue apiEndpoint;
        
        public TariffLimitsClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("TariffsApiEndpoint");
        }

        public Task<TariffLimitDto> GetAsync(int tariffId, ToolType toolType)
        {
            return GetAsync<TariffLimitDto>("/GetBy", new {tariffId, toolType});
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/TariffLimits";
        }
    }
}
