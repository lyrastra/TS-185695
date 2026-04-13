using Moedelo.Authorities.ApiClient.Abstractions;
using Moedelo.Authorities.ApiClient.Abstractions.Sfr;
using Moedelo.Authorities.ApiClient.Abstractions.Sfr.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Threading.Tasks;

namespace Moedelo.Authorities.ApiClient.NetFramework.Sfr
{
    [InjectAsSingleton(typeof(ISfrRegionRequisitesApiClient))]
    public class SfrRegionRequisitesApiClient : BaseCoreApiClient, ISfrRegionRequisitesApiClient
    {
        private readonly SettingValue apiEndPoint;

        public SfrRegionRequisitesApiClient(
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
            apiEndPoint = settingRepository.Get("SfrApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<SfrRegionRequisitesDto> GetByRegionCodeAsync(string regionCode)
        {
            var tokenHeaders = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);

            var response = await GetAsync<ApiDataResult<SfrRegionRequisitesDto>>($"/Regions/{regionCode}/Requisites", queryHeaders: tokenHeaders);
            return response.data;
        }
        
        public async Task<bool> MatchAsync(string settlementAccount, string unifiedSettlementAccount)
        {
            var tokenHeaders = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);

            var response = await GetAsync<ApiDataResult<bool>>($"/Match", new {settlementAccount, unifiedSettlementAccount}, queryHeaders: tokenHeaders);
            return response.data;
        }
    }
}
