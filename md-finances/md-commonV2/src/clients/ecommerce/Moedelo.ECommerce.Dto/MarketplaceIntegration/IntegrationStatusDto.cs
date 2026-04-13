using Moedelo.Common.Enums.Enums.Marketplaces;

namespace Moedelo.ECommerce.Dto.MarketplaceIntegration
{
    public class IntegrationStatusDto
    {
        public MarketplaceType Marketplace { get; set; }

        public IntegrationStatus Status { get; set; }
    }
}
