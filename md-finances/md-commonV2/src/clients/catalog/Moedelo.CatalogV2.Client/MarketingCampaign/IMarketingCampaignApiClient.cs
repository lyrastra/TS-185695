using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.MarketingCampaign;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CatalogV2.Client.MarketingCampaign
{
    public interface IMarketingCampaignApiClient : IDI
    {
        Task<List<MarketingCampaignIdDto>> GetIdListAsync();

        Task<int> CreateIdAsync(MarketingCampaignIdDto campaignIdDto);

        Task<int> SetCampaingToSemgentRelationAsync(
            IReadOnlyCollection<MarketingCampaignIdDto> campaignIdDtoList);
    }
}