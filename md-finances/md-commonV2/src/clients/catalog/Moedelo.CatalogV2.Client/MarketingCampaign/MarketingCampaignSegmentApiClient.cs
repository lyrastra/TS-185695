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
    public class MarketingCampaignSegmentApiClient : BaseApiClient, IMarketingCampaignSegmentApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public MarketingCampaignSegmentApiClient(
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
            return apiEndPoint.Value + "/MarketingCampaignSegment/V2";
        }

        public Task<List<MarketingCampaignSegmentDto>> GetSegmentListAsync()
        {
            return GetAsync<List<MarketingCampaignSegmentDto>>("/GetList", null);
        }

        public Task<int> SaveOrUpdateAsync(MarketingCampaignSegmentDto segmentDto)
        {
            return PostAsync<MarketingCampaignSegmentDto, int>("/SaveOrUpdate", segmentDto);
        }

        public Task DeleteAsync(int id)
        {
            return PostAsync($"/Delete?id={id}");
        }
    }
}