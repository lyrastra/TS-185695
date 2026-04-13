using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Manufacturing.Dto;

namespace Moedelo.Manufacturing.Client
{
    [InjectAsSingleton]
    public class ManufacturingSettingsClient : BaseApiClient, IManufacturingSettingsClient
    {
        private readonly SettingValue apiEndpoint;

        public ManufacturingSettingsClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(httpRequestExecutor,
                uriCreator,
                responseParser,
                auditTracer,
                auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ManufacturingApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }


        public Task<ManufacturingSettingDto> GetAsync(int firmId, int userId)
        {
            return GetAsync<ManufacturingSettingDto>($"/ManufacturingSettings/Get?firmId={firmId}&userId={userId}");
        }
    }
}