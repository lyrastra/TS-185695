using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrationsV2.Dto.IntegratedUser
{
    public class NextAcceptancePriceDto
    {
        public int FirmId { get; set; }
        public int PriceListId { get; set; }
        public IntegrationPartners Partner { get; set; }
        public int UserIdWhoSelectTariff { get; set; }
        public int FirmIdWhoSelectTariff { get; set; }
    }
}
