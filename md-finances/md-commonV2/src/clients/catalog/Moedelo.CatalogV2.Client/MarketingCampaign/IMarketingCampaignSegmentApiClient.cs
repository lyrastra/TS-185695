using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.MarketingCampaign;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CatalogV2.Client.MarketingCampaign
{
    public interface IMarketingCampaignSegmentApiClient : IDI
    {
        Task<List<MarketingCampaignSegmentDto>> GetSegmentListAsync();

        Task<int> SaveOrUpdateAsync(MarketingCampaignSegmentDto segmentDto);

        Task DeleteAsync(int id);
    }
}