using IntegrationPartners = Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums.IntegrationPartners;

namespace Moedelo.BankIntegrationsV2.Dto.IntegratedUser
{
    public class FirmIntegrationPartnerDto
    {
        public int FirmId { get; set; }
        public IntegrationPartners IntegrationPartner { get; set; }
    }
}