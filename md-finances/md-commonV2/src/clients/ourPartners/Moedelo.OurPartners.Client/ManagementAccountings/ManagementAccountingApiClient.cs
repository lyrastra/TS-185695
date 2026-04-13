using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.OurPartners.Dto;
using Moedelo.OurPartners.Dto.ManagementAccountings;

namespace Moedelo.OurPartners.Client.ManagementAccountings
{
    [InjectAsSingleton]
    public class ManagementAccountingApiClient : BaseCoreApiClient, IManagementAccountingApiClient
    {
        private readonly SettingValue apiEndpoint;

        public ManagementAccountingApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("OurPartnersApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public async Task<ManagementAccountingInfoDto> GetInfo(int firmId, int userId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await GetAsync<ApiDto<ManagementAccountingInfoDto>>(
                    $"/api/v1/ManagementAccounting/Info?firmId={firmId}&userId={userId}",
                    queryHeaders: tokenHeaders)
                    .ConfigureAwait(false);
            
            return response.Data;
        }
    }
}