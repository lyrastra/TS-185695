using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser
{
    public class FirmIntegrationPartnerDto
    {
        public int FirmId { get; set; }
        public IntegrationPartners IntegrationPartner { get; set; }
    }
}
