using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser
{
    public class SaveForTesterDto
    {
        public int FirmId { get; set; }
        public string Login { get; set; }
        public IntegrationPartners IntegrationPartner { get; set; }
    }
}
