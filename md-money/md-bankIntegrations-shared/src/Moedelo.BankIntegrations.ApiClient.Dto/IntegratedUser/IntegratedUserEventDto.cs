using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser
{
    public class IntegratedUserEventDto
    {
        public int FirmId { get; set; }

        public IntegrationPartners IntegrationPartner { get; set; }

        public bool IsActive { get; set; }
    }
}
