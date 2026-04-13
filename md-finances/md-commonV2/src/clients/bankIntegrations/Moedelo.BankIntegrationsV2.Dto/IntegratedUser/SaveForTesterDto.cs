using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrationsV2.Dto.IntegratedUser
{
    public class SaveForTesterDto
    {
        public int FirmId { get; set; }
        public string Login { get; set; }
        public IntegrationPartners IntegrationPartner { get; set; }
    }
}