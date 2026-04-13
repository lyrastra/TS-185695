using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.MarketingCampaign;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CatalogV2.Client.MarketingCampaign
{
    [InjectAsSingleton]
    public class MarketingCampaignApiClient : BaseApiClient, IMarketingCampaignApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public MarketingCampaignApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager
                 )
        {
            apiEndPoint = settingRepository.Get("CatalogApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/MarketingCampaign/V2";
        }

        public Task<List<MarketingCampaignIdDto>> GetIdListAsync()
        {
            return GetAsync<List<MarketingCampaignIdDto>>("/GetIdList", null);
        }

        public Task<int> CreateIdAsync(MarketingCampaignIdDto campaignIdDto)
        {
            return PostAsync<MarketingCampaignIdDto, int>("/CreateId", campaignIdDto);
        }

        public Task<int> SetCampaingToSemgentRelationAsync(
            IReadOnlyCollection<MarketingCampaignIdDto> campaignIdDtoList)
        {
            return PostAsync<IReadOnlyCollection<MarketingCampaignIdDto>, int>("/SetCampaingToSemgentRelation",
                campaignIdDtoList);
        }
    }
}